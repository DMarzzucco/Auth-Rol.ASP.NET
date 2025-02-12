using User.Repository;
using User.Repository.Interface;
using User.Service;
using User.Service.Interface;

namespace User.Configurations
{
    public static class ServiceBuilderImplement
    {
        public static IServiceCollection AddServiceScope(this IServiceCollection service) {

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserService, UserService>();

            return service;
        }
    }
}
