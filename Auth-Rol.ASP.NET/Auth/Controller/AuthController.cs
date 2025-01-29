using Auth_Rol.ASP.NET.Auth.Attributes;
using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Filter;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Auth.Controller
{
    [Route("api/[controller]")]
    [JwtAuth]
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
        [AllowAnonymousAccess]
        [ServiceFilter(typeof(LocalAuthFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost ("login")]
        public async Task<ActionResult> Login([FromBody] AuthDTO body)
        {
            var user = HttpContext.Items["User"] as UsersModel;
            var newToken = await this._services.GenerateToken(user);

            return StatusCode(StatusCodes.Status200OK, new { token = newToken });
        }

        /// <summary>
        /// Get Profile
        /// </summary>
        /// <returns>Get User Profile </returns>
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> GetProfile()
        {
            var user = await this._services.GetProfile();
            return Ok(user);
        }

        /// <summary>
        /// Log Out
        /// </summary>
        /// <returns>A Log Out Successfully </returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LogOut()
        {
            await this._services.LogOut();
            return Ok(new { message = "Log Out successfully" });   
        }
    }
}
