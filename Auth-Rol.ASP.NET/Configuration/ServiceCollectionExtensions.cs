using Auth_Rol.ASP.NET.Auth.Filter;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Auth.Services;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Auth_Rol.ASP.NET.Project.Repository;
using Auth_Rol.ASP.NET.Project.Service.Interface;
using Auth_Rol.ASP.NET.Project.Service;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Repository;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Auth_Rol.ASP.NET.Users.Services;
using Auth_Rol.ASP.NET.Utils.Filter;
using Auth_Rol.ASP.NET.UserProject.Repository.Interface;
using Auth_Rol.ASP.NET.UserProject.Repository;
using Auth_Rol.ASP.NET.Auth.JWT.Service.Interface;
using Auth_Rol.ASP.NET.Auth.JWT.Service;
using Auth_Rol.ASP.NET.Auth.Cookie.Service;
using Auth_Rol.ASP.NET.Auth.Cookie.Service.Interface;
using Auth_Rol.ASP.NET.Cache.Infrastucture.Interface;
using Auth_Rol.ASP.NET.Cache.Infrastucture;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //GlobalFilterException
            services.AddScoped<GlobalFilterExceptions>();
            //RolesAuth
            services.AddScoped<RolesAuthentication>();
            //Redis
            services.AddScoped<IRedisService, RedisService>();
            //JwtAuth
            services.AddScoped<JwtAuthFilter>();
            //Local Auth
            services.AddScoped<LocalAuthFilter>();
            //TokenService
            services.AddScoped<ITokenService, TokenService>();
            //CookieService 
            services.AddScoped<ICookieService, CookieService>();
            //AuthSerives
            services.AddScoped<IAuthServices, AuthServices>();
            //UserServices
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserServices>();
            //UserProjectService
            services.AddScoped<IUserProjectRepository, UserProjectRepository>();
            //ProjectService
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}
