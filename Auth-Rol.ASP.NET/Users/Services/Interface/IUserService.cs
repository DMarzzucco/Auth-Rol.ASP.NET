using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Users.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UsersModel>> GetAll();

        Task<UsersModel> GetById(int id);

        Task<UsersModel> CreateUser(CreateUserDTO body);

        Task<bool> UpdateUser(int id, UpdateUserDTO user);

        Task<bool> DeleteUser(int id);

        Task<UsersModel> FindByAuth(string key, object value);
    }
}
