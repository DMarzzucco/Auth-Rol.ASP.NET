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

        /// <summary>
        /// Clean Memory
        /// </summary>
        /// <returns></returns>
        public async Task CleanRedis()
        {
            await this._redis.ExecuteAsync("FLUSHALL");
        }
        /// <summary>
        /// Invalid Pattern
        /// </summary>
        /// <returns></returns>
        public async Task InvalidPattern() {
            var user = "UserModel:*";
            var project = "ProjectModel:*";
            await this.DeleteFromCacheAsync(user, project);
        }
        /// <summary>
        /// Delete From Key Value
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFromCacheAsync(params string[] pattern)
        {
            bool deleted = false;

            foreach (var patterns in pattern)
            {
                var server = this._redis.Multiplexer.GetServer(this._redis.Multiplexer.GetEndPoints()[0]);
                var keys = server.Keys(pattern: patterns).ToArray();

                if (keys.Length > 0)
                {
                    await this._redis.KeyDeleteAsync(keys);
                    deleted = true;
                }
            }
            return deleted;
        }

        /// <summary>
        /// Get Date from Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var data = await this._redis.StringGetAsync(key);

            if (string.IsNullOrEmpty(data)) return default;

            return JsonSerializer.Deserialize<T>(data, JsonSerializerHelper.Default);
        }

        /// <summary>
        /// Save Date in Cache Memory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetToCacheAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializer = JsonSerializer.Serialize(value, JsonSerializerHelper.Default);
            await this._redis.StringSetAsync(key, serializer, TimeSpan.FromMinutes(10));
        }
    }
}
