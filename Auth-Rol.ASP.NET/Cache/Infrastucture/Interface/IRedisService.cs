namespace Auth_Rol.ASP.NET.Cache.Infrastucture.Interface
{
    public interface IRedisService
    {
        Task<T?> GetFromCacheAsync<T>(string key);
        Task SetToCacheAsync<T>(string key, T value, TimeSpan expiration);
        Task DeleteFromCacheAsync(params string[] keys);
    }
}
