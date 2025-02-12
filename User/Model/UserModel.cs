using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using User.Enums;

namespace User.Model
{
    public class UserModel
    {
        public int Id { get; set; }

        //[SwaggerSchema("User FirstName")]
        //[SwaggerSchemaExample("Dario")]
        public required string First_name { get; set; }

        //[SwaggerSchema("User Lastname")]
        //[SwaggerSchemaExample("Marzzucco")]
        public required string Last_name { get; set; }

        //[SwaggerSchema("User Age")]
        //[SwaggerSchemaExample("26")]
        public required string Age { get; set; }

        //[SwaggerSchema("User username")]
        //[SwaggerSchemaExample("Darmarz")]
        public required string Username { get; set; }

        //[SwaggerSchema("User  Email")]
        //[SwaggerSchemaExample("darmarz@gmail.com")]
        [EmailAddress]
        public required string Email { get; set; }

        //[SwaggerSchema("User Password")]
        //[SwaggerSchemaExample("prometheus98")]
        public required string Password { get; set; }

        //[SwaggerSchema("User Rol")]
        //[SwaggerSchemaExample("ADMIN")]
        public required ROLES Roles { get; set; }

        [SwaggerIgnore]
        public string? RefreshToken { get; set; }
    }
}
