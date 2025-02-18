using Auth.Cookies.Interface;
using Auth.JWT.DTOs;

namespace Auth.Cookies
{
    public class CookieService : ICookieService
    {
        public void ClearTokenCookies(HttpResponse response)
        {
            SetCookie(response, "Authentication", "", DateTime.UnixEpoch);
            SetCookie(response, "RefreshToken", "", DateTime.UnixEpoch);
        }

        public void SetTokenCookies(HttpResponse response, TokenPair tokens)
        {
            SetCookie(response, "Authentication", tokens.AccessToken, DateTime.UtcNow.AddHours(2));
            SetCookie(response, "RefreshToken", tokens.RefreshToken, DateTime.UtcNow.AddDays(5));
        }

        private void SetCookie(
            HttpResponse response,
            string name,
            string value,
            DateTime expiration
            )
        {
            response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly= true,
                Secure = true,
                Expires = expiration,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
