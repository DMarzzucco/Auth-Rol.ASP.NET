using Auth_Rol.ASP.NET.Configuration;
using Auth_Rol.ASP.NET.Configuration.DbConfiguration;
using Auth_Rol.ASP.NET.Configuration.Swagger;

namespace Auth_Rol.ASP.NET.Extensions
{
    public static class ServiciesBuilderExtensions
    {
        public static IServiceCollection AddServicesBuilder(this IServiceCollection services, IConfiguration configuration) {
            //SQL Connection
            services.AddDatabaseConfiguration(configuration);
            // Redis Connection
            services.AddSingletonExtesions();
            //Add httpContext
            services.AddHttpContextAccessor();
            //Cors Policy
            services.AddCorsPolicy();
            //JwtBuilderConfigure
            services.AddJwtAuthentication(configuration);
            //Register Filter
            services.AddCustomController();
            // Register Services
            services.AddCustomServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfiguration();
            //Mapper
            services.AddMapperConfig();
            services.AddMvc();
            return services;
        }
    }
}
