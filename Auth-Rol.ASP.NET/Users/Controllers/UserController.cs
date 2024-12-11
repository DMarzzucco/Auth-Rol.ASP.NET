using Auth_Rol.ASP.NET.Auth.Attribute;
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

        /// <summary>
        /// Register a new User
        /// </summary>
        /// <returns>A new User Register in DB</returns>
        /// <response code = "201">User register successfully</response>
        /// <response code = "409">Repeat Username o Email </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UsersModel>> RegisterUser([FromBody] CreateUserDTO user)
        {
            var body = await this._service.CreateUser(user);
            return CreatedAtAction(nameof(GetAllUsers), new { id = body.Id }, body);
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>Get a List of All Users</returns>
        /// <response code = "200">List of Users</response>
        /// <response code = "400"> Bad Request</response>
        [JwtAuth]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UsersModel>>> GetAllUsers()
        {
            return Ok(await this._service.GetAll());
        }

        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>Return a user by id</returns>
        /// <response code = "200">Users</response>
        /// <response code = "404">User not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersModel>> GetUserById(int id)
        {
            var user = await this._service.GetById(id);
            return Ok(user);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code = "204">No Content</response>
        /// <response code = "404">User not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO body)
        {
            await this._service.UpdateUser(id, body);
            return NoContent();
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code = "204">No Content</response>
        /// <response code = "404">User not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await this._service.DeleteUser(id);
            return NoContent();
        }

    }
}
