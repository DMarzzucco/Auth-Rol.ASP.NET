using Swashbuckle.AspNetCore.Annotations;
using Auth_Rol.ASP.NET.Filter.Attributes;

namespace Auth_Rol.ASP.NET.Auth.DTO
{
    public class AuthDTO
    {
        [SwaggerSchema("User Username")]
        [SwaggerSchemaExample("Darmarz")]
        public required string Username { get; set; }

        [SwaggerSchema("User Password")]
        [SwaggerSchemaExample("prometheus98")]
        public required string Password { get; set; }
    }
}
