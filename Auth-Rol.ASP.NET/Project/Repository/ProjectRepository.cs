using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Auth_Rol.ASP.NET.UserProject.Model;
using Microsoft.EntityFrameworkCore;

using Auth_Rol.ASP.NET.Users.Model;
using Pipelines.Sockets.Unofficial.Arenas;
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
                .AsNoTracking()
                .Include(p => p.UsersIncludes)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (project == null) return null;

            project.UsersIncludes ??= new List<UsersProjectModel>();
            foreach (var userProject in project.UsersIncludes)
            {
                this._context.Entry(userProject.User).Collection(p => p.ProjectsIncludes).Load();
            }

            // serializa y alamacena el resultado en cache 
            await this._redis.SetToCacheAsync(cacheKey, project, TimeSpan.FromMinutes(10));

            return project;
        }

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(ProjectModel body)
        {
            var existingEntities = _context.ChangeTracker
                    .Entries<UsersModel>()
                    .Where(e => e.Entity.ProjectsIncludes.Any(up => up.ProjectId == body.Id))
                    .ToList();

            foreach (var entity in existingEntities)
            {
                _context.Entry(entity.Entity).State = EntityState.Detached;
            }
            //remove project 
            var project = await _context.ProjectModel
                .AsNoTracking()
                .Include(p => p.UsersIncludes)
                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == body.Id);
            if (project == null) return false;

            this._context.ProjectModel.Remove(project);
            await this._context.SaveChangesAsync();

            // Eliminar el proyecto de Redis
            var redisKey = $"ProjectModel:{body.Id}";
            var redisList = "ProjectModel:List";
            await this._redis.DeleteFromCacheAsync(redisKey, redisList);

            return true;
        }

        /// <summary>
        /// Save project
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SaveProjectAsync(ProjectModel body)
        {
            var existingProject = await _context.ProjectModel
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == body.Id);
            if (existingProject != null)
            {
                this._context.Entry(body).State = EntityState.Modified;
            }
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

            await this._redis.SetToCacheAsync(cacheKey, project, TimeSpan.FromMinutes(10));

            return project;
        }
        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ProjectModel body)
        {
            var existingEntities = _context.ChangeTracker
                .Entries<UsersModel>()
                .Where(e => e.Entity.ProjectsIncludes.Any(up => up.ProjectId == body.Id))
                .ToList();

            foreach (var entity in existingEntities)
            {
                _context.Entry(entity.Entity).State = EntityState.Detached;
            }
            //edit project
            var project = await _context.ProjectModel
                .AsNoTracking()
                .Include(p => p.UsersIncludes)
                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == body.Id);
            if (project == null) return false;

            //Actualizacion de la base de datos
            this._context.Entry(project).State = EntityState.Detached;
            this._context.ProjectModel.Entry(body).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            //redis
            var redisKeyId = $"ProjectModel:{body.Id}";
            var redisList = "ProjectModel:List";

            await this._redis.DeleteFromCacheAsync(redisKeyId, redisList);
            return true;
        }
    }
}
