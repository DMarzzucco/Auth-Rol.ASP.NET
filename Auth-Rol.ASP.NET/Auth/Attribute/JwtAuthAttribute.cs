using Auth_Rol.ASP.NET.Auth.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Auth.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthAttribute: TypeFilterAttribute
    {
        public JwtAuthAttribute(): base (typeof (JwtAuthFilter))
        {
            
        }
    }
}
