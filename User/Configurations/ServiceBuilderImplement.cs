using User.Module.Repository;
using User.Module.Repository.Interface;
using User.Module.Service;
using User.Module.Service.Interface;
using User.Utils.Filter;

namespace User.Configurations
{
    public static class ServiceBuilderImplement
    {
        public static IServiceCollection AddServiceScope(this IServiceCollection service) {

            service.AddScoped<GlobalFilterExceptions>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserService, UserService>();

            return service;
        }
    }
}
