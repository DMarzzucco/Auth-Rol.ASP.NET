namespace Auth_Rol.ASP.NET.Auth.Cookie.Helper
{
    public static class CookieHelper
    {
        public static void SetCookie(
            HttpResponse response,
            string name,
            string value,
            DateTime expiration)
        {
            response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = expiration,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
