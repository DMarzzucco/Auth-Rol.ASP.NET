using Auth_Rol.ASP.NET.Auth.Cookie.Service.Interface;
using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.JWT.Service.Interface;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Auth_Rol.ASP.NET.Auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public AuthServices(IConfiguration config, IUserService userService, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, ICookieService cookieService)
        {
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
            this._tokenService = tokenService;
            this._cookieService = cookieService;
        }

        // Generate Token
        public async Task<string> GenerateToken(UsersModel body)
        {
            var token = this._tokenService.GenerateToken(body);
            await this._userService.updateToken(body.Id, token.RefreshTokenHashed);

            this._cookieService.SetTokenCookies(this._httpContextAccessor.HttpContext.Response, token);

            return token.AccessToken;
        }
        // Generate Token
        public async Task<string> RefreshToken()
        {
            var user = await GetProfileByCookie();
            var token = this._tokenService.GenerateToken(user);
            await this._userService.updateToken(user.Id, token.RefreshTokenHashed);

            this._cookieService.SetTokenCookies(this._httpContextAccessor.HttpContext.Response, token);

            return token.AccessToken;
        }

        //Validate User 
        public async Task<UsersModel> ValidationUser(AuthDTO body)
        {
            var user = await this._userService.FindByAuth("Username", body.Username);
            var passwordHasher = new PasswordHasher<UsersModel>();

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, body.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Password wrong");
            }

            return user;
        }

        //RefreshTokenValidate
        public async Task<UsersModel> RefreshTokenValidate(string refreshToken, int id)
        {
            var user = await this._userService.GetById(id);

            var match = BCrypt.Net.BCrypt.Equals(refreshToken, user.RefreshToken);
            if (match == null) throw new UnauthorizedAccessException();

            return user;
        }

        //LogOut
        public async Task LogOut()
        {
            var user = await this.GetProfileByCookie();
            await this._userService.updateToken(user.Id, null);

            this._cookieService.ClearTokenCookies(this._httpContextAccessor.HttpContext.Response);
        }


        //Get Profile
        public async Task<string> GetProfile()
        {
            var user = await this.GetProfileByCookie();
            return user.Username;
        }

        //Get User 
        public async Task<UsersModel> GetProfileByCookie()
        {
            var userId = this._tokenService.GetIdFromToken();
            var user = await this._userService.GetById(userId);
            if (user == null) throw new UnauthorizedAccessException("User date is invalid");
            return user;
        }
    }
}
