using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.Project.DTO
{
    public class UpdateProjectDTO
    {
        [SwaggerSchema("Project Name")]
        [SwaggerSchemaExample("Next year's projects.")]
        public string? Name { get; set; }

        [SwaggerSchema("Project Description")]
        [SwaggerSchemaExample("Next year's projects include basic tasks for beginners, as well as a review of errors made the previous year.")]
        public string? Description { get; set; }
    }
}
