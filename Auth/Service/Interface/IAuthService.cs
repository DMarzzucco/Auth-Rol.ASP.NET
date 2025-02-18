using Auth.DTOs;
using Auth.User.Model;

namespace Auth.Service.Interface
{
    public interface IAuthService
    {
        Task<UserModel> ValidateUser(AuthDTO body);
        Task<string> RefreshToken();
        Task<string> GenerateToken(UserModel body);
        Task<UserModel> GetUserbyCookie();
        Task<string> GetProfile();
        Task<UserModel> RefreshTokenValidate(string refreshToken, int id);
        Task LogOut();
    }
}
