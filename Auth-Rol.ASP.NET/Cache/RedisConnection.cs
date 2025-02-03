using StackExchange.Redis;

namespace Auth_Rol.ASP.NET.Cache
{
    public static class RedisConnection
    {
        public static IServiceCollection AddRedisConnection(this IServiceCollection service)
        {

            service.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var redisConnect = configuration.GetConnectionString("Redis");

                if (string.IsNullOrEmpty(redisConnect))
                    throw new ArgumentException("Redis connection string is not configured. Check your appsettings.json");

                return ConnectionMultiplexer.Connect(redisConnect);
            });

            return service;
        }
    }
}
