using Gateway.Configurations;
using Gateway.Configurations.Ocelot;
using Gateway.Configurations.Swagger;

namespace Gateway.Extensions
{
    public static class ServiciesBuilderExtensions
    {
        public static IServiceCollection AddServicesBuilder(this IServiceCollection service, IConfiguration configuration) {

            service.AddCustomController();
            service.AddServicesScopeExtensions();
            service.AddSwaggerConfiguration();
            service.AddOcelotConfiguration();

            return service;
        }
    }
}
