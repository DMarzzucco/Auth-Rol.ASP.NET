using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Services.Interface;
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


        // Generate Token
        public Task<string> GenerateToken(UsersModel body)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

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
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDecriptor);

            var accessToken = tokenHandler.WriteToken(tokenConfig);

            this._httpContextAccessor.HttpContext.Response.Cookies.Append("Authentication", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(2),
                SameSite = SameSiteMode.Strict

            });

            return Task.FromResult(accessToken);
        }

        public async Task<UsersModel> GetProfile() 
        {
            var token = this._httpContextAccessor.HttpContext.Request.Cookies["Authentication"];

            if (token == null) throw new UnauthorizedAccessException("Token not found");

        }
    }
}
