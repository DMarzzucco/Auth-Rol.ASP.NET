using Auth_Rol.ASP.NET.Auth.Filter;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Auth.Services;
using Auth_Rol.ASP.NET.Filter;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Auth_Rol.ASP.NET.Project.Repository;
using Auth_Rol.ASP.NET.Project.Service.Interface;
using Auth_Rol.ASP.NET.Project.Service;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Repository;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Auth_Rol.ASP.NET.Users.Services;

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
            //JwtAuth
            services.AddScoped<JwtAuthFilter>();
            //AuthSerives
            services.AddScoped<LocalAuthFilter>();
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
