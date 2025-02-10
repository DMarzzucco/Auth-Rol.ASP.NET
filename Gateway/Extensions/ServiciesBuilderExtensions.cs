using Gateway.Configurations.Ocelot;
using Gateway.Configurations.Swagger;

namespace Gateway.Extensions
{
    public static class ServiciesBuilderExtensions
    {
        public static IServiceCollection AddServicesBuilder(this IServiceCollection service, IConfiguration configuration) {

            service.AddSwaggerConfiguration();
            service.AddOcelotConfiguration();

            return service;
        }
    }
}
