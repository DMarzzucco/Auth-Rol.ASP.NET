using Auth_Rol.ASP.NET.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            service.AddDbContext<AppDbContext>(op => op.UseNpgsql(connectionString));

            return service;
        }
    }
}
