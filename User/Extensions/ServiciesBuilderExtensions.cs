using User.Configurations.Swagger;

namespace User.Extensions
{
    public static class ServiciesBuilderExtensions
    {
        public static IServiceCollection AddServicesBuilder(this IServiceCollection service, IConfiguration configuration) {

            service.AddSwaggerConfiguration();

            return service;
        }
    }
}
