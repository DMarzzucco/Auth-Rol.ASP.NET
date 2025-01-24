using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.UserProject.Repository.Interface;
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
            //var TrackUserA = await this._context.UserModel
            //.Include(u => u.ProjectsIncludes)
            //    .ThenInclude(u => u.Project)
            //    .FirstOrDefaultAsync(u => u.Id == body.Id);

            //if (TrackUserA == null) return false;

            //this._context.Entry(TrackUserA).State = EntityState.Detached;

            //var TrackProject = await this._context.ProjectModel
            //    .Include(p => p.UsersIncludes)
            //    .ThenInclude(u => u.User)
            //    .FirstOrDefaultAsync(u => u.Id == body.Id);
            //if (TrackProject == null) return false;

            //this._context.Entry(TrackProject).State = EntityState.Detached;

            var user = await _context.UserModel
                .AsNoTracking()
                .Include(u => u.ProjectsIncludes)
                .ThenInclude(up => up.Project)
                .FirstOrDefaultAsync(u => u.Id == body.UserId);

            if (user == null) return false;

            var project = await _context.ProjectModel
                .AsNoTracking()
                .Include(p => p.UsersIncludes)
                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == body.ProjectId);

            if (user == null || project == null) return false;

            // Verificar si la relación ya existe
            if (user.ProjectsIncludes.Any(up => up.ProjectId == body.ProjectId)) return false;

            _context.Attach(user);
            _context.Attach(project);

            this._context.UsersProject.Add(body);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
