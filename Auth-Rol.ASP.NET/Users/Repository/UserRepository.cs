using Auth_Rol.ASP.NET.Cache.Infrastucture.Interface;
using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Users.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly IRedisService _redis;
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context, IRedisService redis)
        {
            this._context = context;
            this._redis = redis;
        }

        /// <summary>
        /// FindByIdAsync 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UsersModel?> FindByIdAsync(int id)
        {

            //string cacheKey = $"UserModel:{id}";
            //var user = await this._redis.GetFromCacheAsync<UsersModel>(cacheKey);
            //if (user != null) return user;

            var user = await this._context.UserModel
                .AsNoTracking()
                .Include(u => u.ProjectsIncludes)
                .ThenInclude(pi => pi.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            user.ProjectsIncludes ??= new List<UsersProjectModel>();

            //await this._redis.SetToCacheAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            //
            return user;
        }
        /// <summary>
        /// To list async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UsersModel>> ToListAsync()
        {
            //string cacheKey = "UserModel:List";
            //var user = await this._redis.GetFromCacheAsync<IEnumerable<UsersModel>>(cacheKey);

            //if (user != null) return user;

            var user = await this._context.UserModel.Select(u => new UsersModel
            {
                Id = u.Id,
                First_name = u.First_name,
                Last_name = u.Last_name,
                Age = u.Age,
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,
                Roles = u.Roles,
                RefreshToken = u.RefreshToken,
                ProjectsIncludes = new List<UsersProjectModel>()
            }).ToListAsync();

            //await this._redis.SetToCacheAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            //
            return user;
        }
        /// <summary>
        /// Exist Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistsByEmail(string email)
        {
            return this._context.UserModel.Any(u => u.Email == email);
        }
        /// <summary>
        /// Exists Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ExistsByUsername(string username)
        {
            return this._context.UserModel.Any(u => u.Username == username);
        }
        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(UsersModel date)
        {
            // actions
            var user = await this._context.UserModel
                .Include(p => p.ProjectsIncludes)
                .ThenInclude(up => up.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == date.Id);

            if (user == null) return false;

            this._context.UserModel.Remove(user);
            await this._context.SaveChangesAsync();
            //
            //var redisId = $"UserModel:{date.Id}";
            //var redisList = "UserModel:List";
            //await this._redis.DeleteFromCacheAsync(redisId, redisList);
            //
            return true;
        }
        /// <summary>
        /// Save User register
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddChangeAsync(UsersModel data)
        {
            this._context.UserModel.Add(data);
            await this._context.SaveChangesAsync();
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UsersModel data)
        {
            // edti user
            var user = await this._context.UserModel.FirstOrDefaultAsync(u => u.Id == data.Id);

            if (user == null) return false;

            var existingUser = this._context.UserModel.Local.FirstOrDefault(u => u.Id == data.Id);

            if (existingUser != null)
                this._context.Entry(existingUser).State = EntityState.Detached;

            this._context.Attach(data);            

            this._context.UserModel.Entry(data).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            //
            //var redisId = $"UserModel:{data.Id}";
            //var redisList = "UserModel:List";
            //await this._redis.DeleteFromCacheAsync(redisId, redisList);
            //
            return true;
        }
        /// <summary>
        /// FindByKey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<UsersModel?> FindByKey(string key, object value)
        {
            var user = await this._context.UserModel
                .AsQueryable()
                .Where(u => EF.Property<object>(u, key).Equals(value))
                .SingleOrDefaultAsync();
            return user;
        }
    }
}
