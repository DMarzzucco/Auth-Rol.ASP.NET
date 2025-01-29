using Auth_Rol.ASP.NET.Configuration.DbConfiguration.Helper;
using Auth_Rol.ASP.NET.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Configuration.DbConfiguration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            using (var serviceProvider = service.BuildServiceProvider())
            {
                var logger = serviceProvider.GetRequiredService<ILogger<object>>();

                WaitConnection.WaitForDatabaseAsync(connectionString, logger).GetAwaiter().GetResult();
            }
            service.AddDbContext<AppDbContext>(op =>
            {
                op.UseNpgsql(connectionString);
                if (configuration.GetValue<string>("ASPNETCORE_ENVIROMENT") == "Development")
                {
                    op.EnableSensitiveDataLogging();
                }
            });

            return service;
        }
    }
}
