using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {

        private readonly IUserService _service;
        public UserProjectController(IUserService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Relation Project
        /// </summary>
        /// <returns>Relation User and Project with a access level</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterUserAndProject(UsersProjectDTO body)
        {
            var data = await this._service.relationProject(body);
            return Ok(data);
        }
    }
}
