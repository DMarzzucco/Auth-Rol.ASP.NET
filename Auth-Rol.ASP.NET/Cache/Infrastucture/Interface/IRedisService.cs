namespace Auth_Rol.ASP.NET.Cache.Infrastucture.Interface
{
    public interface IRedisService
    {
        Task CleanRedis();
        Task DeleteFromCacheAsync(params string[] keys);
        Task<T?> GetFromCacheAsync<T>(string key);
        Task InvalidateCacheByPatternAsync(string pattern);
        Task SetToCacheAsync<T>(string key, T value);
        Task<List<string>> SetScanKeyAsync(string pattern);
    }
}
