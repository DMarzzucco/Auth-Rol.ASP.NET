using Auth_Rol.ASP.NET.Project.DTO;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Service.Interface;
using Auth_Rol.ASP.NET.UserProject.Model;
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

        /// <summary>
        /// Create a Project
        /// </summary>
        /// <returns>Give a new Project</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsersProjectModel>> CreateProject([FromBody] CreateProjectDTO body)
        {
            var project = await this._service.saveProject(body);
            return CreatedAtAction(nameof(GetAllProject), new { id = project.Id }, project);
        }

        /// <summary>
        /// Get All Project
        /// </summary>
        /// <returns>List of Project</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProject()
        {
            var project = await this._service.getAllProject();
            return Ok(project);
        }

        /// <summary>
        /// Get a Project
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return a Project</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectModel>> GetAProjectById(int id)
        {
            var project = await this._service.getProjectById(id);
            return Ok(project);
        }

        /// <summary>
        /// Update a project
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="body">Body</param>
        /// <returns>No content</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectModel>> UpdateProject(int id, [FromBody] UpdateProjectDTO body)
        {
            await this._service.updateProject(id, body);
            return NoContent();
        }

        /// <summary>
        /// Delete Project
        /// </summary>
        /// <returns>No content</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProject(int id)
        {
            await this._service.delteProject(id);
            return NoContent();
        }
    }
}
