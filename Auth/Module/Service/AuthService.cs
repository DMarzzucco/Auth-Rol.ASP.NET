using Auth.Cookies.Interface;
using Auth.JWT.Interface;
using Auth.Module.DTOs;
using Auth.Module.Service.Interface;
using Auth.User.Model;

namespace Auth.Module.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;

        public AuthService(IHttpContextAccessor context, IJwtService jwtService, ICookieService cookieService)
        {
            _context = context;
            _jwtService = jwtService;
            _cookieService = cookieService;
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GenerateToken(UserModel body)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetProfile()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get user by Cookie
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<UserModel> GetUserbyCookie()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log Out
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> RefreshToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// RefreshTokenValidate
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<UserModel> RefreshTokenValidate(string refreshToken, int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<UserModel> ValidateUser(AuthDTO body)
        {
            throw new NotImplementedException();
        }
    }
}
