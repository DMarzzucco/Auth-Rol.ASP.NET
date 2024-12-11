using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Auth.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<UsersModel> ValidationUser(AuthDTO body);

        Task<string> GenerateToken(UsersModel body);

        Task<UsersModel> GetUserProfile();

        Task<string> GetProfile();

        Task<UsersModel> RefreshTokenValidate(string refreshToken, int id);

        Task LogOut();
    }
}
