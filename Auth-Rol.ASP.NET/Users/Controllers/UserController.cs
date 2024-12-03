using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UsersModel>> CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                var body = await this._service.CreateUser(user);
                return CreatedAtAction(nameof(GetAllUsers), new { id = body.Id }, body);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>> GetAllUsers()
        {
            return Ok(await this._service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersModel>> GetUserById(int id)
        {
            try
            {
                var user = await this._service.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO body)
        {
            if (!await this._service.UpdateUser(id, body))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await this._service.DeleteUser(id))
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
