using Auth_Rol.ASP.NET.Auth.JWT.DTOs;
using Auth_Rol.ASP.NET.Auth.JWT.Helper;
using Auth_Rol.ASP.NET.Auth.JWT.Service.Interface;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_Rol.ASP.NET.Auth.JWT.Service
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _context;

        public TokenService(IConfiguration config, IHttpContextAccessor context)
        {
            this._secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
            this._context = context;
        }
        public TokenPair GenerateToken(UsersModel user)
        {
            return CreateTokenPair(
                user,
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddDays(5)
                );
        }

        public TokenPair RefreshTokenGenerate(UsersModel user)
        {
            return CreateTokenPair(
                user,
                DateTime.UtcNow.AddDays(5),
                DateTime.UtcNow.AddDays(5));
        }

        public int GetIdFromToken()
        {
            var token = this._context.HttpContext.Request.Cookies["Authentication"];
            if (token == null) throw new UnauthorizedAccessException("Token not found");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var id = int.Parse(jwtToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            if (id == null) throw new UnauthorizedAccessException("Invalid Token");

            return id;
        }

        public bool isExpireTokenSoon(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token)) return false;

            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var expiration = jwtToken.ValidFrom;
            return expiration <= DateTime.UtcNow.AddMinutes(20);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(this._secretKey);

            var principal = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                tokenHandler.ValidateToken(token, principal, out _);
                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                return false;
            }
        }
        public TokenPair CreateTokenPair(UsersModel user, DateTime accessTokenExpire, DateTime refreshTokenExpire)
        {
            var keyBites = Encoding.UTF8.GetBytes(_secretKey);
            var signing = new SigningCredentials(new SymmetricSecurityKey(keyBites), SecurityAlgorithms.HmacSha256Signature);

            var claim = new List<Claim> {
                new Claim("sub", user.Id.ToString()),
                new Claim ("role", user.Roles.ToString())
            };
            var accessToken = CreateTokenHelper.CreateToken(claim, signing, accessTokenExpire);
            var refreshToken = CreateTokenHelper.CreateToken(claim, signing, accessTokenExpire);

            var refreshTokenHasher = BCrypt.Net.BCrypt.HashPassword(refreshToken);

            return new TokenPair
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenHashed = refreshTokenHasher
            };
        }
    }
}
