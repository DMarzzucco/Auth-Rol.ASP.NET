using Auth_Rol.ASP.NET.Auth.Middleware;

namespace Auth_Rol.ASP.NET.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UserApplicationBuilderExtension(this IApplicationBuilder app) {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<RefreshTokenMIddleware>();
            return app;
        }
    }
}
