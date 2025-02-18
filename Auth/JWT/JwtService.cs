using Auth.JWT.DTOs;
using Auth.JWT.Interface;
using Auth.User.Model;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.JWT
{
    public class JwtService : IJwtService
    {
        private readonly string _seecretKey;
        private readonly IHttpContextAccessor _context;

        public JwtService(IConfiguration config, IHttpContextAccessor context)
        {
            this._seecretKey = config.GetSection("JwtSettings").GetSection("seecretKey").ToString();
            this._context = context;
        }

        /// <summary>
        /// Get Token from ID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
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

        /// <summary>
        ///  Generate Access_Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenPair GenerateToken(UserModel user)
        {
            return CreateTokenPair(
                user,
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddDays(5)
                );
        }

        /// <summary>
        /// Generate Refresh_Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenPair RefreshToken(UserModel user)
        {
            return CreateTokenPair(
                user,
                DateTime.UtcNow.AddDays(5),
                DateTime.UtcNow.AddDays(5)
                );
        }

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(this._seecretKey);

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
            catch (SecurityTokenExpiredException)
            {
                return false;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify the token expiration
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool isTokenExpirationSoon(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token)) return false;

            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null) return false;

            var expiration = jwtToken.ValidTo;
            return expiration <= DateTime.UtcNow.AddMinutes(21);
        }

        /// <summary>
        /// Template to Create Token Pair
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accessTokenExpired"></param>
        /// <param name="refreshTokenExpired"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TokenPair CreateTokenPair(UserModel user, DateTime accessTokenExpired, DateTime refreshTokenExpired)
        {
            var keyBytes = Encoding.UTF8.GetBytes(this._seecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim> { 
                new Claim("sub", user.Id.ToString()),
                new Claim ("rol", user.Roles.ToString())
            };

            var accessToken = CreateToken(claims, credentials, accessTokenExpired);
            var refreshToken = CreateToken(claims, credentials, refreshTokenExpired);

            var refreshTokenHasher = BCrypt.Net.BCrypt.HashPassword(refreshToken);

            return new TokenPair {
                AccessToken= accessToken,
                RefreshToken = refreshToken,
                RefreshTokenHasher = refreshTokenHasher
            };
        }

        /// <summary>
        /// Template to Create Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="signing"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        private string CreateToken(IEnumerable<Claim> claims, SigningCredentials signing, DateTime expiration)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = signing
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
