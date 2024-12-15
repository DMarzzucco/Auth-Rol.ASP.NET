using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;

namespace Auth_Rol.ASP.NET.Users.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly AppDbContext _context;
        public UserProjectRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task AddChangeAsync(UsersProjectModel body)
        {
            this._context.Add(body);
            await this._context.SaveChangesAsync();
        }
    }
}
