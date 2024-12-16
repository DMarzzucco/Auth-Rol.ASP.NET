using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;

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

        public async Task RemoveAsync(UsersModel user)
        {
            this._context.UserModel.Remove(user);
            await this._context.SaveChangesAsync();
        }

        public async Task AddChangeAsync(UsersModel data)
        {
            this._context.UserModel.Add(data);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsersModel data)
        {
            this._context.UserModel.Entry(data).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<UsersModel?> FindByKey(string key, object value)
        {
            var user = await this._context.UserModel
                .AsQueryable()
                .Where(u => EF.Property<object>(u, key).Equals(value))
                .SingleOrDefaultAsync();
            //var parameter = Expression.Parameter(typeof(UsersModel), "user");
            //var property = Expression.Property(parameter, key);
            //var constant = Expression.Constant(value);
            //var equality = Expression.Equal(property, constant);
            //var predicate = Expression.Lambda<Func<UsersModel, bool>>(equality, parameter);

            //return await _context.UserModel
            //    .Where(predicate)
            //    .SingleOrDefaultAsync();
            return user;
        }
    }
}
