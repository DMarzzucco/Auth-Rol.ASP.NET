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

        public AuthServices(IConfiguration config, IUserService userService)
        {
            this.secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
            this._userService = userService;
        }

        //Validate User 
        public async Task<UsersModel> ValidationUser(AuthDTO body)
        {
            var user = await this._userService.FindByAuth("Username", body.Username);
            var passwordHasher = new PasswordHasher<UsersModel>();

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, body.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Password wrong");
            }
            return user;
        }

        // Generate Token
        public Task<string> GenerateToken(string Username )
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Username));

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDecriptor);

            return Task.FromResult(tokenHandler.WriteToken(tokenConfig));
        }
    }
}
