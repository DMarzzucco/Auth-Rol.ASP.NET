using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Auth_Rol.ASP.NET.UserProject.Model;
using Microsoft.EntityFrameworkCore;

using Auth_Rol.ASP.NET.Cache.Infrastucture.Interface;

namespace Auth_Rol.ASP.NET.Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IRedisService _redis;
        public ProjectRepository(AppDbContext context, IRedisService redis)
        {
            this._context = context;
            this._redis = redis;
        }

        /// <summary>
        /// Fin by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectModel?> FinByIdAsync(int id)
        {
            string cacheKey = $"ProjectModel:{id}";
            var project = await this._redis.GetFromCacheAsync<ProjectModel>(cacheKey);

            if (project != null) return project;
            
            project = await this._context.ProjectModel
                .Include(p => p.UsersIncludes)
                .ThenInclude(u => u.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (project == null) return null;

            project.UsersIncludes ??= new List<UsersProjectModel>();

            // serializa y alamacena el resultado en cache 
            await this._redis.SetToCacheAsync(cacheKey, project);

            return project;
        }

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(ProjectModel body)
        {
            //remove project 
            this._context.ProjectModel.Remove(body);
            await this._context.SaveChangesAsync();

            // Eliminar el proyecto de Redis
            //var redisKey = $"ProjectModel:{body.Id}";
            //var redisList = "ProjectModel:List";
            //await this._redis.DeleteFromCacheAsync(redisKey, redisList);
            await this._redis.CleanRedis();
            return true;
        }

        /// <summary>
        /// Save project
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SaveProjectAsync(ProjectModel body)
        {
            this._context.ProjectModel.Add(body);
            await this._context.SaveChangesAsync();

            return true;
        }
        /// <summary>
        /// To list async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectModel>> ToListAsync()
        {

            string cacheKey = "ProjectModel:List";
            var project = await this._redis.GetFromCacheAsync<IEnumerable<ProjectModel>>(cacheKey);

            if (project != null) return project;

            project = await this._context.ProjectModel.Select(p => new ProjectModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                UsersIncludes = new List<UsersProjectModel>()
            }).ToListAsync();

            await this._redis.SetToCacheAsync(cacheKey, project);

            return project;
        }
        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ProjectModel body)
        {

            //edit project
            this._context.ProjectModel.Entry(body).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            //redis
            //var redisKeyId = $"ProjectModel:{body.Id}";
            //var redisList = "ProjectModel:List";
            //await this._redis.DeleteFromCacheAsync(redisKeyId, redisList);
            await this._redis.CleanRedis();
            return true;
        }
    }
}
