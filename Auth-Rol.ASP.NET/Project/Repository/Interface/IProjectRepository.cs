using Auth_Rol.ASP.NET.Project.Model;

namespace Auth_Rol.ASP.NET.Project.Repository.Interface
{
    public interface IProjectRepository
    {
        Task<bool> SaveProjectAsync(ProjectModel body);

        Task<IEnumerable<ProjectModel>> ToListAsync();

        Task<ProjectModel?> FinByIdAsync(int id);

        Task<bool> UpdateAsync(ProjectModel body);

        Task<bool> RemoveAsync(ProjectModel body);
    }
}
