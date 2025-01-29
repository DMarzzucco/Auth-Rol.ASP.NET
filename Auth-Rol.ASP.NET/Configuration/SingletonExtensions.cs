using StackExchange.Redis;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class SingletonExtensions
    {
        public static IServiceCollection AddSingletonExtesions(this IServiceCollection service)
        {
            service.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var redisConnection = configuration.GetConnectionString("Redis");
                if (string.IsNullOrEmpty(redisConnection))
                {
                    throw new ArgumentException("Redis connection string is not configured. Check your appsettings.json");
                }
                return ConnectionMultiplexer.Connect(redisConnection);
            });
            return service;
        }
    }
}
