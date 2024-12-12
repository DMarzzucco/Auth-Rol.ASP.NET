using Auth_Rol.ASP.NET.Auth.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Rol.ASP.NET.Auth.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthRolesAttribute:TypeFilterAttribute
    {
        public AuthRolesAttribute():base(typeof (RolesAuthentication))
        {
            
        }
    }
}
