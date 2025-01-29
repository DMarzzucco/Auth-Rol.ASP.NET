using Auth_Rol.ASP.NET.UserProject.Model;

namespace Auth_Rol.ASP.NET.UserProject.Repository.Interface
{
    public interface IUserProjectRepository
    {
        Task<bool> AddChangeAsync(UsersProjectModel body);
    }
}
