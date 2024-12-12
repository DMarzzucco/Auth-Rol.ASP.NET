using Auth_Rol.ASP.NET.Auth.Attributes;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth_Rol.ASP.NET.Auth.Filter
{
    public class JwtAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthServices _authServices;
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAuthFilter(IConfiguration config, IHttpContextAccessor httpContextAccessor, IAuthServices authService)
        {
            this._secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
            this._authServices = authService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasAllowAnonymousAccess = context.ActionDescriptor.EndpointMetadata.Any(md => md is AllowAnonymousAccessAttribute);

            if (hasAllowAnonymousAccess) return;

            var token = this._httpContextAccessor.HttpContext.Request.Cookies["Authentication"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "Invalid or not provided token.",
                    ContentType = "application/json"
                };
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(this._secretKey);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var user = await this._authServices.GetUserProfile();

            this._httpContextAccessor.HttpContext.Items["UserId"] = user.Id;
            this._httpContextAccessor.HttpContext.Items["UserRole"] = user.Roles;
        }
    }
}
