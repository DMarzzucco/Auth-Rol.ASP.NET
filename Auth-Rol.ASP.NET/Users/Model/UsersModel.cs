using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Auth_Rol.ASP.NET.UserProject.Model;
using Auth_Rol.ASP.NET.Users.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Auth_Rol.ASP.NET.Users.Model
{
    public class UsersModel
    {
        [SwaggerSchema("User id is uniqued")]
        public int Id { get; set; }

        [SwaggerSchema("User firtname")]
        [SwaggerSchemaExample("Dario")]
        public required string First_name { get; set; }

        [SwaggerSchema("User Lastname")]
        [SwaggerSchemaExample("Marzzucco")]
        public required string Last_name { get; set; }

        [SwaggerSchema("User Age")]
        [SwaggerSchemaExample("26")]
        public required string Age { get; set; }

        [SwaggerSchema("User username")]
        [SwaggerSchemaExample("Darmarz")]
        public required string Username { get; set; }

        [SwaggerSchema("User  Email")]
        [SwaggerSchemaExample("darmarz@gmail.com")]
        [EmailAddress]
        public required string Email { get; set; }

        [SwaggerSchema("User Password")]
        [SwaggerSchemaExample("prometheus98")]
        public required string Password { get; set; }

        [SwaggerSchema("User Rol")]
        [SwaggerSchemaExample("ADMIN")]
        public required ROLES Roles { get; set; }

        [SwaggerIgnore]
        public string? RefreshToken { get; set; }

        [SwaggerIgnore]
        public ICollection<UsersProjectModel> ProjectsIncludes { get; set; } = new List<UsersProjectModel>();
    }
}
