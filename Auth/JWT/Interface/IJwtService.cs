using Auth.JWT.DTOs;
using Auth.User.Model;

namespace Auth.JWT.Interface
{
    public interface IJwtService
    {
        TokenPair GenerateToken(UserModel user);
        TokenPair RefreshToken(UserModel user);
        bool ValidateToken(string token);
        int GetIdFromToken();
        bool isTokenExpirationSoon(string token);
        TokenPair CreateTokenPair(UserModel user, DateTime accessTokenExpired, DateTime refreshTokenExpired);
    }
}
