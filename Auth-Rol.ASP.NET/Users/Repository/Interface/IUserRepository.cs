using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Users.Repository.Interface
{
    public interface IUserRepository
    {
        Task SaveChangeAsync();

        Task<UsersModel?> FindByIdAsync(int id);

        Task<IEnumerable<UsersModel>> ToListAsync();

        bool ExistsByEmail(string email);

        bool ExistsByUsername(string username);

        Task<UsersModel?> FindAsync();

        Task RemoveAsync(UsersModel user);

        Task AddChangeAsync(UsersModel data);

        Task Entry(UsersModel data);

        Task<UsersModel?> FindByKey(string key, object value);
    }
}
