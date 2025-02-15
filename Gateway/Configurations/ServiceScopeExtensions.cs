using Gateway.Utils.Filter;

namespace Gateway.Configurations
{
    public static class ServiceScopeExtensions
    {
        public static IServiceCollection AddServicesScopeExtensions(this IServiceCollection service) {

            service.AddTransient<GlobalFilterExeption>();

            return service;
        }

    }
}
