using Auth_Rol.ASP.NET.Auth.JWT.DTOs;

namespace Auth_Rol.ASP.NET.Auth.Cookie.Service.Interface
{
    public interface ICookieService
    {
        void SetTokenCookies(HttpResponse response, TokenPair tokens);
        void ClearTokenCookies(HttpResponse response);
    }
}
