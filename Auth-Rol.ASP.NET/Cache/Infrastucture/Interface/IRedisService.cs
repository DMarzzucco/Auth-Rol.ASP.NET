namespace Auth_Rol.ASP.NET.Cache.Infrastucture.Interface
{
    public interface IRedisService
    {
        Task CleanRedis();
        Task<T?> GetFromCacheAsync<T>(string key);
        Task SetToCacheAsync<T>(string key, T value);
        Task DeleteFromCacheAsync(params string[] keys);
    }
}
