using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Auth.Configuration
{
    public static class JWTConfiguration
    {
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection service, IConfiguration configuration)
        {

            var secretKey = configuration.GetSection("JwtSettings").GetSection("seecretKey").ToString();

            service.AddAuthentication(conf =>
            {

                conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(conf =>
            {
                var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var singingCredential = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256Signature);

                conf.RequireHttpsMetadata = false;
                conf.SaveToken = true;
                conf.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey =true,
                    IssuerSigningKey = singingKey,
                    ValidateIssuer = false, 
                    ValidateAudience = false
                };
            });

            return service;
        }
    }
}
