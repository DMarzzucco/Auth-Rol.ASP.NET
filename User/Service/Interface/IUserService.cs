using User.DTOs;
using User.Model;

namespace User.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> RegisterUser(CreateUserDTO body);
        Task<UserModel> UpdateUser(int id, UpdateUserDTO body);
        Task DelteUser(int id);
    }
}
