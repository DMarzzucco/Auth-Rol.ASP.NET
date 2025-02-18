using Auth.JWT.DTOs;

namespace Auth.Cookies.Interface
{
    public interface ICookieService
    {
        void SetTokenCookies(HttpResponse response, TokenPair tokens);
        void ClearTokenCookies(HttpResponse response);
    }
}
