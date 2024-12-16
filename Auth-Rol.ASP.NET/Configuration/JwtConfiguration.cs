namespace Auth_Rol.ASP.NET.Configuration
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings").GetSection("secretKey").ToString();

            return service;
        }
    }
}
