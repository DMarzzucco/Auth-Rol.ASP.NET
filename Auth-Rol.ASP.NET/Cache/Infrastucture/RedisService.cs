using Auth_Rol.ASP.NET.Cache.Infrastucture.Interface;
using Auth_Rol.ASP.NET.Utils.Helper;
using StackExchange.Redis;
using System.Text.Json;

namespace Auth_Rol.ASP.NET.Cache.Infrastucture
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redis;
        public RedisService(IConnectionMultiplexer redis)
        {
            this._redis = redis.GetDatabase();
        }

        public async Task DeleteFromCacheAsync(params string[] keys)
        {
            foreach (var key in keys)
            {
                await this._redis.KeyDeleteAsync(key);
            }
        }
        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var data = await this._redis.StringGetAsync(key);

            if (string.IsNullOrEmpty(data)) return default;
            return JsonSerializer.Deserialize<T>(data, JsonSerializerHelper.Default);
        }
        public async Task SetToCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializer = JsonSerializer.Serialize(value, JsonSerializerHelper.Default);
            await this._redis.StringSetAsync(key, serializer,expiration);
        }
    }
}
