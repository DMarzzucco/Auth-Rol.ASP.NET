using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Users.Repository.Interface
{
    public interface IUserProjectRepository
    {
        Task AddChangeAsync(UsersProjectModel body);
    }
}
