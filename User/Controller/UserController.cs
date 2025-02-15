using Microsoft.AspNetCore.Mvc;
using User.DTOs;
using User.Model;
using User.Service.Interface;

namespace User.Controller
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

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] CreateUserDTO body)
        {

            var date = await this._service.RegisterUser(body);
            return CreatedAtAction(nameof(GetAllUser), new { id = date.Id }, date);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUser()
        {
            return Ok(await this._service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await this._service.GetById(id);
            return Ok(user);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO body)
        {
            await this._service.UpdateUser(id, body);
            return NoContent();
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            await this._service.DelteUser(id);
            return NoContent();
        }
    }
}
