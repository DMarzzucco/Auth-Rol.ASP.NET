using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
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
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] AuthDTO body)
        {
            try
            {
                var user = await this._services.ValidationUser(body);

                var newToken = await this._services.GenerateToken(user);

                return StatusCode(StatusCodes.Status200OK, new { token = newToken });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
    }
}
