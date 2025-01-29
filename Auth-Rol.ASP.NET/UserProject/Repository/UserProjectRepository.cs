using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.UserProject.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.UserProject.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly AppDbContext _context;
        public UserProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddChangeAsync(UsersProjectModel body)
        {
            var user = await _context.UserModel.AsNoTracking().FirstOrDefaultAsync(u => u.Id == body.UserId);

            var project = await _context.ProjectModel.AsNoTracking().FirstOrDefaultAsync(p => p.Id == body.ProjectId);

            if (user == null || project == null) return false;

            var entityExisting = this._context.UsersProject.Local.FirstOrDefault(u => u.UserId == body.UserId && u.ProjectId == body.ProjectId);

            if (entityExisting != null) return false;

            var trackedUser = _context.ChangeTracker.Entries<UsersModel>()
                .FirstOrDefault(e => e.Entity.Id == user.Id);

            if (trackedUser != null)
            {
                _context.Entry(trackedUser.Entity).State = EntityState.Detached;
            }

            var trackedProject = _context.ChangeTracker.Entries<ProjectModel>()
                .FirstOrDefault(e => e.Entity.Id == project.Id);

            if (trackedProject != null)
            {
                _context.Entry(trackedProject.Entity).State = EntityState.Detached;
            }
            
            this._context.Attach(user);
            this._context.Attach(project);

            this._context.UsersProject.Add(body);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
