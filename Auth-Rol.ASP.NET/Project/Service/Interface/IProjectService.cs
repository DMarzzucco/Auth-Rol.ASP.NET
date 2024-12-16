using Auth_Rol.ASP.NET.Project.DTO;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Users.Model;

namespace Auth_Rol.ASP.NET.Project.Service.Interface
{
    public interface IProjectService
    {

        Task<UsersProjectModel> saveProject(CreateProjectDTO body);

        Task<IEnumerable<ProjectModel>> getAllProject();

        Task<ProjectModel> getProjectById(int id);

        Task <ProjectModel> updateProject(int id, UpdateProjectDTO body);

        Task delteProject(int id);
    }
}
