using Auth_Rol.ASP.NET.Users.Enums;

namespace Auth_Rol.ASP.NET.Auth.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RolesAccessAttribute : Attribute
    {
        public ROLES[] Roles { get; }

        public RolesAccessAttribute(params ROLES[] roles) { this.Roles = roles; }

    }
}
