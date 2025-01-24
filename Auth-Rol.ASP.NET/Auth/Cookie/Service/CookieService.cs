using Auth_Rol.ASP.NET.Auth.Cookie.Helper;
using Auth_Rol.ASP.NET.Auth.Cookie.Service.Interface;
using Auth_Rol.ASP.NET.Auth.JWT.DTOs;

namespace Auth_Rol.ASP.NET.Auth.Cookie.Service
{
    public class CookieService : ICookieService
    {
        public void ClearTokenCookies(HttpResponse response)
        {
            CookieHelper.SetCookie(response, "Authentication", "", DateTime.UnixEpoch);
            CookieHelper.SetCookie(response, "RefreshToken", "", DateTime.UnixEpoch);
        }

        public void SetTokenCookies(HttpResponse response, TokenPair tokens)
        {
            CookieHelper.SetCookie(response, "Authentication", tokens.AccessToken, DateTime.UtcNow.AddDays(5));
            CookieHelper.SetCookie(response, "RefreshToken", tokens.RefreshToken, DateTime.UtcNow.AddDays(5));
        }
    }
}
