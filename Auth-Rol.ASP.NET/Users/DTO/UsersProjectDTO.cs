using Auth_Rol.ASP.NET.Filter.Attributes;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Users.Enums;
using Auth_Rol.ASP.NET.Users.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.Users.DTO
{
    public class UsersProjectDTO
    {
        [SwaggerSchema("Access Level")]
        [SwaggerSchemaExample("OWNER")]
        public required AccesLevel AccessLevel { get; set; }

        [SwaggerSchema("User id ")]
        public required UsersModel User { get; set; }

        [SwaggerSchema("User id")]
        public required ProjectModel Project { get; set; }
    }
}
