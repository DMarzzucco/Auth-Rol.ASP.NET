using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Auth_Rol.ASP.NET.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string secretKey;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            this.secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
            this._userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] AuthDTO body)
        {
            //user validate
            var user = await this._userService.FindByAuth("Username", body.Username);
            var passwordHasher = new PasswordHasher<UsersModel>();

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, body.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Password wrong");
            }
            // generate token
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, body.Username));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string newToken = tokenHandler.WriteToken(tokenConfig);

            return StatusCode(StatusCodes.Status200OK, new { token = newToken});
        }
    }
}
