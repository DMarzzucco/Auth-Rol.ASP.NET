using Auth_Rol.ASP.NET.Auth.Cookie.Service.Interface;
using Auth_Rol.ASP.NET.Auth.JWT.Service.Interface;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Rol.ASP.NET.Auth.Middleware
{
    public class RefreshTokenMIddleware
    {
        private readonly RequestDelegate _next;

        public RefreshTokenMIddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenService tokenService, ICookieService cookieService, IAuthServices authService)
        {
            var publicPath = new[] { "/api/Auth/login", "/api/User/register" };

            var path = context.Request.Path.Value;
            if (publicPath.Contains(path))
            {
                await _next(context);
                return;
            }
            var accessToken = context.Request.Cookies["Authentication"];

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "token is missing" });
                return;
            };
            try
            {
                if (!tokenService.ValidateToken(accessToken))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new { message = "The Token is invalid" });
                    return;
                }
                if (tokenService.isExpireTokenSoon(accessToken))
                {
                    var user = await authService.GetProfileByCookie();
                    var Refreshtoken = context.Request.Cookies["RefreshToken"];
                    if (string.IsNullOrEmpty(Refreshtoken))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsJsonAsync(new { message = "Token is Missing" });
                    }
                    if (!tokenService.ValidateToken(Refreshtoken))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsJsonAsync(new { message = "Refresh Token is Invalid" });
                    }
                    var newAccessToken = tokenService.RefreshTokenGenerate(user);
                    cookieService.SetTokenCookies(context.Response, newAccessToken);
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
                return;
            }
            await this._next(context);
        }
    }
}
