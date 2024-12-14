using Auth_Rol.ASP.NET.Filter.Attributes;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Users.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.Users.Model
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

        [SwaggerIgnore]
        public required UsersModel User { get; set; }

        [SwaggerIgnore]
        public int ProjectId { get; set; }

        [SwaggerIgnore]
        public required ProjectModel Project { get; set; }
    }
}
