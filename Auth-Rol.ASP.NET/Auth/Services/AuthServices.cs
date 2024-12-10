using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Migrations;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_Rol.ASP.NET.Auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly string secretKey;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthServices(IConfiguration config, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        // Generate Token
        public async Task<string> GenerateToken(UsersModel body)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim("sub", body.Id.ToString()),
                new Claim("role", body.Roles.ToString())
            };

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToeknDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenConfig = tokenHandler.CreateToken(tokenDecriptor);
            var refreshTokenConf = tokenHandler.CreateToken(refreshToeknDescriptor);

            var accessToken = tokenHandler.WriteToken(tokenConfig);
            var refreshToken = tokenHandler.WriteToken(refreshTokenConf);

            var hashRefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken, 10);

            await this._userService.updateToken(body.Id, hashRefreshToken);

            this._httpContextAccessor.HttpContext.Response.Cookies.Append("Authentication", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(2),
                SameSite = SameSiteMode.Strict

            });
            this._httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Strict
            });

            return accessToken;
        }

        //Validate User 
        public async Task<UsersModel> ValidationUser(AuthDTO body)
        {
            var user = await this._userService.FindByAuth("Username", body.Username);
            var passwordHasher = new PasswordHasher<UsersModel>();

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, body.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Password wrong");
            }

            return user;
        }

        public async Task<string> GetProfile()
        {
            var user = await this.GetUserProfile();
            return user.Username;
        }

        //Get User 
        public async Task<UsersModel> GetUserProfile()
        {
            var token = this._httpContextAccessor.HttpContext.Request.Cookies["Authentication"];
            if (token == null) throw new UnauthorizedAccessException("Token not found");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = int.Parse(jwtToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            if (userId == null)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            var user = await this._userService.GetById(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            return user;

        }
    }
}
