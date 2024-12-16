using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<ProjectModel?> FinByIdAsync(int id)
        {
            return await this._context.ProjectModel.FindAsync(id);
        }

        public async Task RemoveAsync(ProjectModel body)
        {
            this._context.ProjectModel.Remove(body);
            await this._context.SaveChangesAsync();
        }

        public async Task SaveProjectAsync(ProjectModel body)
        {
            this._context.ProjectModel.Add(body);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectModel>> ToListAsync()
        {
            return await this._context.ProjectModel.ToListAsync();
        }

        public async Task UpdateAsync(ProjectModel body)
        {
            this._context.ProjectModel.Entry(body).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}
