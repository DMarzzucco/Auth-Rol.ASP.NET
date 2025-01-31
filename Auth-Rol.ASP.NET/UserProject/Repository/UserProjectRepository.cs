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
        /// <summary>
        /// AddChangeAsync 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> AddChangeAsync(UsersProjectModel body)
        {
            var project = await this._context.ProjectModel.FindAsync(body.ProjectId);
            var user = await this._context.UserModel.FindAsync(body.UserId);

            if (project == null || user == null)
                return false;

            var entityExisting = this._context.UsersProject.Local.FirstOrDefault(u => u.Id == body.Id);

            if (entityExisting != null)
                this._context.Entry(entityExisting).State = EntityState.Detached;

            this._context.Attach(user);
            this._context.Attach(project);

            this._context.UsersProject.Add(body);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
