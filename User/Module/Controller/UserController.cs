using Microsoft.AspNetCore.Mvc;
using User.Module.DTOs;
using User.Module.Model;
using User.Module.Service.Interface;

namespace User.Module.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] CreateUserDTO body)
        {

            var date = await _service.RegisterUser(body);
            return CreatedAtAction(nameof(GetAllUser), new { id = date.Id }, date);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUser()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _service.GetById(id);
            return Ok(user);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO body)
        {
            await _service.UpdateUser(id, body);
            return NoContent();
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            await _service.DelteUser(id);
            return NoContent();
        }
    }
}
