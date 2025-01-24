using Auth_Rol.ASP.NET.UserProject.DTOs;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.UserProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {

        private readonly IUserService _service;
        public UserProjectController(IUserService service)
        {
            _service = service;
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
            var data = await _service.relationProject(body);
            return Ok(data);
        }
    }
}
