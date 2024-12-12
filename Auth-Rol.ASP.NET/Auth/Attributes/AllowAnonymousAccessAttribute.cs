
namespace Auth_Rol.ASP.NET.Auth.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple =false, Inherited = true)]
    public class AllowAnonymousAccessAttribute:Attribute
    {
        public AllowAnonymousAccessAttribute()
        {
            
        }
    }
}
