using Auth_Rol.ASP.NET.UserProject.DTOs;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Users.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UsersModel>> GetAll();

        Task<UsersModel> GetById(int id);

        Task<UsersModel> CreateUser(CreateUserDTO body);

        Task<UsersModel> UpdateUser(int id, UpdateUserDTO user);

        Task DeleteUser(int id);

        Task<UsersModel> FindByAuth(string key, object value);

        Task<UsersModel> updateToken(int id, string RefreshToken);

        Task<UsersProjectModel> relationProject(UsersProjectDTO body);
    }
}
