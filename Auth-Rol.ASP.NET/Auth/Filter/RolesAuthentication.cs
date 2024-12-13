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

            var userRoleString = context.HttpContext.Items["UserRole"]?.ToString();

            if (string.IsNullOrEmpty(userRoleString))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "Empty Rol",
                    ContentType = "application/json"
                };
                return;
            }

            if (!Enum.TryParse(userRoleString, out ROLES userRole))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "Invalid Role",
                    ContentType = "application/json"
                };
                return;
            }
            if (userRole == ROLES.ADMIN) return;

            var requiredRoles = context.ActionDescriptor.EndpointMetadata
                            .OfType<RolesAccessAttribute>()
                            .FirstOrDefault()?.Roles;

            bool isAuthorized = requiredRoles.Any(role => (int)userRole <= (int)role);

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