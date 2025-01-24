using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth_Rol.ASP.NET.Auth.JWT.Helper
{
    public static class CreateTokenHelper
    {
        public static string CreateToken(IEnumerable<Claim> claim, SigningCredentials signing, DateTime expiration)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = expiration,
                SigningCredentials = signing
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
