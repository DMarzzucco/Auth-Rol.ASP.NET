using Auth_Rol.ASP.NET.Auth.DTO;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth_Rol.ASP.NET.Auth.Filter
{
    public class LocalAuthFilter : IAsyncActionFilter
    {
        private readonly IAuthServices _authService;

        public LocalAuthFilter(IAuthServices authService)
        {
            this._authService = authService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var body = context.ActionArguments["body"] as AuthDTO;

            var user = await this._authService.ValidationUser(body);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            context.HttpContext.Items["User"] = user;

            await next();
        }
    }
}
