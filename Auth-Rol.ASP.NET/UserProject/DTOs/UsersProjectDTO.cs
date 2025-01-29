using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.UserProject.Enum;
using Auth_Rol.ASP.NET.Users.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.UserProject.DTOs
{
    public class UsersProjectDTO
    {
        [SwaggerSchema("Access Level")]
        [SwaggerSchemaExample("OWNER")]
        public required AccesLevel AccessLevel { get; set; }

        [SwaggerSchema("User")]
        public required int UserId { get; set; }

        [SwaggerSchema("Project")]
        public required int ProjectId { get; set; }

    }
}
