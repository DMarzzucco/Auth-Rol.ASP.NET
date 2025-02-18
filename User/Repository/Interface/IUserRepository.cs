using User.Model;

namespace User.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserModel?> FindByIdAsync(int id);
        Task<IEnumerable<UserModel>> ToListAsync();
        bool ExistsByEmail(string email);
        bool ExistsByUsername(string username);
        Task<bool> RemoveAsync(UserModel date);
        Task AddChangeAsync(UserModel date);
        Task<bool> UpdateAsync(UserModel date);
        Task<UserModel?> FindByKey(string key, object value);
    }
}
