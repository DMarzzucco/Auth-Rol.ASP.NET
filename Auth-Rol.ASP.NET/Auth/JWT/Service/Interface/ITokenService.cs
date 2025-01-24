using Auth_Rol.ASP.NET.Auth.JWT.DTOs;
using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Auth.JWT.Service.Interface
{
    public interface ITokenService
    {
        TokenPair GenerateToken(UsersModel user);
        TokenPair RefreshTokenGenerate(UsersModel user);
        bool ValidateToken(string token);
        int GetIdFromToken();
        bool isExpireTokenSoon(string token);
        TokenPair CreateTokenPair(UsersModel user, DateTime accessTokenExpire, DateTime refreshTokenExpire);
    }
}
