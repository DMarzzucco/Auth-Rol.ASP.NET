using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Auth_Rol.ASP.NET.Users.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth_Rol.ASP.NET.Users.DTO
{
    public class CreateUserDTO
    {

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
        public required string Email { get; set; }

        [SwaggerSchema("User Password")]
        [SwaggerSchemaExample("prometheus98")]
        public required string Password { get; set; }

        [SwaggerSchema("User Rol")]
        [SwaggerSchemaExample("ADMIN")]
        public required ROLES Roles { get; set; }
    }
}
