namespace Auth_Rol.ASP.NET.Cache.Infrastucture.Interface
{
    public interface IRedisService
    {
        Task CleanRedis();
        Task<bool> DeleteFromCacheAsync(params string[] pattern);
        Task<T?> GetFromCacheAsync<T>(string key);
        Task InvalidPattern();
        Task SetToCacheAsync<T>(string key, T value);
    }
}
