using Auth_Rol.ASP.NET.Filter;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Auth_Rol.ASP.NET.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(op =>
            {
                op.EnableAnnotations();
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Auth Rols",
                    Description = "An ASP.NET Core Web Apit Auth Controller"
                });
                op.SchemaFilter<SwaggerSchemaExampleFilter>();

                var xmLFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                op.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmLFileName));
            });

            return services;
        }
    }
}
