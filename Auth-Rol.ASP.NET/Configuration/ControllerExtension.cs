using Auth_Rol.ASP.NET.Filter;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddCustomController(this IServiceCollection service)
        {
            service.AddControllers(op =>
            {
                op.Filters.Add<GlobalFilterExceptions>();
            });
            return service;
        }
    }
}
