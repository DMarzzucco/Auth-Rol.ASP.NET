using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Auth_Rol.ASP.NET.Users.Repository
{
  
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task SaveChangeAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task<UsersModel?> FindByIdAsync(int id)
        {
            return await this._context.UserModel.FindAsync(id);
        }

        public async Task<IEnumerable<UsersModel>> ToListAsync()
        {
            return await this._context.UserModel.ToListAsync();
        }

        public bool ExistsByEmail(string email)
        {
            return this._context.UserModel.Any(u => u.Email == email);
        }

        public bool ExistsByUsername(string username)
        {
            return this._context.UserModel.Any(u => u.Username == username);
        }

        public async Task<UsersModel?> FindAsync()
        {
            return await this._context.UserModel.FindAsync();
        }

        public async Task SaveChangesAsync ()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task RemoveAsync (UsersModel user)
        {
            this._context.UserModel.Remove(user);
            await this._context.SaveChangesAsync();
        }

        public async Task AddChangeAsync(UsersModel data) 
        {
            this._context.Add(data);
            await this._context.SaveChangesAsync();
        }

        public async Task Entry (UsersModel data)
        {
            this._context.Entry(data).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}
