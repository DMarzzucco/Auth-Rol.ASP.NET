using User.Configurations;
using User.Configurations.DBConnect;
using User.Configurations.Swagger;

namespace User.Extensions
{
    public static class ServiciesBuilderExtensions
    {
        public static IServiceCollection AddServicesBuilder(this IServiceCollection service, IConfiguration configuration) {

            service.AddDatabaseConfiguration(configuration);
            service.AddServiceScope();

            service.AddEndpointsApiExplorer();
            service.AddSwaggerConfiguration();

            service.AddAutoMapperConfig();
            service.AddMvc();

            return service;
        }
    }
}
