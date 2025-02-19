using System.ComponentModel.DataAnnotations;
using User.Module.Enums;

namespace User.Module.DTOs
{
    public class UpdateUserDTO
    {
        //[SwaggerSchema("User FirstName")]
        //[SwaggerSchemaExample("Dario")]
        public string? First_name { get; set; }

        //[SwaggerSchema("User Lastname")]
        //[SwaggerSchemaExample("Marzzucco")]
        public string? Last_name { get; set; }

        //[SwaggerSchema("User Age")]
        //[SwaggerSchemaExample("26")]
        public string? Age { get; set; }

        //[SwaggerSchema("User username")]
        //[SwaggerSchemaExample("Darmarz")]
        public string? Username { get; set; }

        //[SwaggerSchema("User  Email")]
        //[SwaggerSchemaExample("darmarz@gmail.com")]
        [EmailAddress]
        public string? Email { get; set; }

        //[SwaggerSchema("User Password")]
        //[SwaggerSchemaExample("prometheus98")]
        public string? Password { get; set; }

        //[SwaggerSchema("User Rol")]
        //[SwaggerSchemaExample("ADMIN")]
        public ROLES? Roles { get; set; }
    }
}
