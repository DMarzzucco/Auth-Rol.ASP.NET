using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Filter;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _services;

        public AuthController(IAuthServices services)
        {
            this._services = services;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <returns>User token</returns>
        [ServiceFilter(typeof(LocalAuthFilter))]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] AuthDTO body)
        {
            var user = HttpContext.Items["User"] as UsersModel;
            var newToken = await this._services.GenerateToken(user);

            return StatusCode(StatusCodes.Status200OK, new { token = newToken });
        }
    }
}
