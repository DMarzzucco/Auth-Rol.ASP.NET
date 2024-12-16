using Auth_Rol.ASP.NET.Project.DTO;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Service.Interface;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        public ProjectController(IProjectService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UsersProjectModel>> CreateProject([FromBody] CreateProjectDTO body)
        {
            await this._service.saveProject(body);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProject()
        {
            var project = await this._service.getAllProject();
            return Ok(project);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject(int id)
        {
            await this._service.delteProject(id);
            return NoContent();
        }
    }
}
