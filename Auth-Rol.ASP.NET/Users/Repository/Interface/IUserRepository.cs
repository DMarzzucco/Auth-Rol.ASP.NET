using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Users.Repository.Interface
{
    public interface IUserRepository
    {

        Task<UsersModel?> FindByIdAsync(int id);

        Task<IEnumerable<UsersModel>> ToListAsync();

        bool ExistsByEmail(string email);

        bool ExistsByUsername(string username);

        Task<bool> RemoveAsync(UsersModel date);

        Task AddChangeAsync(UsersModel data);

        Task<bool> UpdateAsync(UsersModel data);

        Task<UsersModel?> FindByKey(string key, object value);
    }
}
