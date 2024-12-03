using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] AuthDTO body)
        {

            return NoContent();
        }
    }
}
