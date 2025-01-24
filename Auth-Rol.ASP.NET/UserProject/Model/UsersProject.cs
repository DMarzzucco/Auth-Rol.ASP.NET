using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.UserProject.Enum;
using Auth_Rol.ASP.NET.Users.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.UserProject.Model
{
    public class UsersProjectModel
    {
        [SwaggerSchema("UserProject id is uniqued")]
        public int Id { get; set; }

        [SwaggerSchema("Access Level")]
        [SwaggerSchemaExample("OWNER")]
        public required AccesLevel AccesLevel { get; set; }

        [SwaggerIgnore]
        public int UserId { get; set; }

        [SwaggerSchema("User")]
        [SwaggerSchemaExample("User")]
        public required UsersModel User { get; set; }

        [SwaggerIgnore]
        public int ProjectId { get; set; }

        [SwaggerSchema("Project")]
        [SwaggerSchemaExample("Project")]
        public required ProjectModel Project { get; set; }
    }
}
