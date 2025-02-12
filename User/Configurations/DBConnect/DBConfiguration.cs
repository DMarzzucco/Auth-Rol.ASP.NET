using Microsoft.EntityFrameworkCore;
using User.Configurations.DBConnect.Helper;
using User.Context;

namespace User.Configurations.DBConnect
{
    public static class DBConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection service, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("Connection");

            using (var serviceProvider = service.BuildServiceProvider())
            {
                var logger = serviceProvider.GetRequiredService<ILogger<object>>();

                WaitForIt.WaitForDatabaseAsync(connectionString, logger).GetAwaiter().GetResult();
            }

            service.AddDbContext<AppDBContext>(op =>
            {
                op.UseNpgsql(connectionString);

                if (configuration.GetValue<string>("ASPNETCORE_ENVIROMENT") == "Development")
                    op.EnableSensitiveDataLogging();
            });
            return service;
        }
    }
}
