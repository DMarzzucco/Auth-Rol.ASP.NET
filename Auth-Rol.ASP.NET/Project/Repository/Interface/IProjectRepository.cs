using Auth_Rol.ASP.NET.Project.Model;

namespace Auth_Rol.ASP.NET.Project.Repository.Interface
{
    public interface IProjectRepository
    {
        Task SaveProjectAsync(ProjectModel body);

        Task<IEnumerable<ProjectModel>> ToListAsync();

        Task<ProjectModel?> FinByIdAsync(int id);

        Task UpdateAsync(ProjectModel body);

        Task RemoveAsync(ProjectModel body);
    }
}
