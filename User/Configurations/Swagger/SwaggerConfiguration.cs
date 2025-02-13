using Microsoft.OpenApi.Models;

namespace User.Configurations.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API User",
                    Version = "v1",
                    Description = "User CRUD"
                });
            });

            return services;
        }
    }
}
