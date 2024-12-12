using Auth_Rol.ASP.NET.Auth.Attributes;
using Auth_Rol.ASP.NET.Users.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth_Rol.ASP.NET.Auth.Filter
{
    public class RolesAuthentication : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAllowAnonymousAccess = context.ActionDescriptor.EndpointMetadata.Any(md => md is AllowAnonymousAccessAttribute);

            if (hasAllowAnonymousAccess) return;

            var userRole = context.HttpContext.Items["UserRole"]?.ToString();

            if (string.IsNullOrEmpty(userRole))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "Empty Rol",
                    ContentType = "application/json"
                };
                return;
            }
            if (userRole == ROLES.ADMIN.ToString()) return;

            var requiredRoles = context.ActionDescriptor.EndpointMetadata
                            .OfType<RolesAccessAttribute>()
                            .FirstOrDefault()?.Roles;

            bool isAuthorized = requiredRoles.Any(role => Enum.GetName(typeof(ROLES), role) == userRole);

            if (!isAuthorized)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "You Rol not get access",
                    ContentType = "application/json"
                };
            }
        }
    }
}