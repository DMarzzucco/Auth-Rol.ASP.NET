using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Gateway.Configurations.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
                op.AddServer(new OpenApiServer { Url = "http://localhost:4080/swagger/v1/swagger.json" });
            });

            return services;
        }
    }
}
