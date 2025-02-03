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
        /// Delete From Key Value
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task DeleteFromCacheAsync(params string[] keys)
        {
            foreach (var key in keys)
            {
                await this._redis.KeyDeleteAsync(key);
            }
        }

        /// <summary>
        /// Get Date from Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var data = await this._redis.StringGetAsync(key);
            if (string.IsNullOrEmpty(data)) return default;
            return JsonSerializer.Deserialize<T>(data, JsonSerializerHelper.Default);
        }
        /// <summary>
        /// Invalida Key in cache
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task InvalidateCacheByPatternAsync(string pattern)
        {

            var keys = await this.SetScanKeyAsync(pattern);
            if (keys.Any())
                await this.DeleteFromCacheAsync(keys.ToString());
        }

        /// <summary>
        /// Set Scan 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task<List<string>> SetScanKeyAsync(string pattern)
        {
            var keys = new List<string>();
            var cursor = 0;

            do
            {
                var scanResult = (RedisResult[]) await this._redis.ExecuteAsync("SCAN", cursor.ToString(), "MATCH", pattern, "COUNT", "100");

                cursor = int.Parse(scanResult[0].ToString());

                var foundKeys = ((RedisResult[])scanResult[1])
                    .Select(k => k.ToString())
                    .ToList();

                keys.AddRange(foundKeys);

            } while (cursor != 0);

            return keys;
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
            var serializer = JsonSerializer.Serialize(value, JsonSerializerHelper.Default);
            await this._redis.StringSetAsync(key, serializer, TimeSpan.FromMinutes(10));
        }
    }
}
