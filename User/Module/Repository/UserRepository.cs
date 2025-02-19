using Microsoft.EntityFrameworkCore;
using User.Context;
using User.Module.Model;
using User.Module.Repository.Interface;

namespace User.Module.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task AddChangeAsync(UserModel date)
        {
            _context.UserModel.Add(date);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exist a same email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistsByEmail(string email)
        {
            return _context.UserModel.Any(u => u.Email == email);
        }

        /// <summary>
        /// Exist a same username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ExistsByUsername(string username)
        {
            return _context.UserModel.Any(u => u.Username == username);
        }

        /// <summary>
        /// Find By Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<UserModel?> FindByKey(string key, object value)
        {
            var user = await _context.UserModel
                .AsQueryable()
                .Where(u => EF.Property<object>(u, key).Equals(value))
                .SingleOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Find By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel?> FindByIdAsync(int id)
        {
           var user= await _context.UserModel.FirstOrDefaultAsync(u=>u.Id == id);

            if (user == null) return null;

            return user;
        }

        /// <summary>
        /// Remove Async
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(UserModel date)
        {
            _context.UserModel.Remove(date);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// To List Async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> ToListAsync()
        {
            return await _context.UserModel.ToListAsync();
        }

        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserModel date)
        {
            _context.UserModel.Entry(date).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
