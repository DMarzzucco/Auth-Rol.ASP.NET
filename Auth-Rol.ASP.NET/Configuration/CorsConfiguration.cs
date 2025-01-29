namespace Auth_Rol.ASP.NET.Configuration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection service)
        {
            service.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", b =>
                {
                    b.WithOrigins("http://localhost:300.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            return service;
        }
    }
}
