using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Utils.Filters
{
    public class GlobalFilterExceptions : IExceptionFilter
    {
        private readonly ILogger<GlobalFilterExceptions> _logger;

        public GlobalFilterExceptions(ILogger<GlobalFilterExceptions> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                UnauthorizedAccessException => 401,
                SecurityTokenExpiredException => 401,
                SecurityTokenSignatureKeyNotFoundException => 401,
                _ => 500
            };

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    401 => context.Exception switch
                    {
                        SecurityTokenExpiredException => "Token has expired",
                        SecurityTokenSignatureKeyNotFoundException => "Invalid Token",
                        _ => context.Exception.Message
                    },
                    _ => context.Exception.Message
                },
                Details = statusCode == 500 ?
                    context.Exception.InnerException?.Message : null
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }

        public class ErrorResponse
        {
            public int StatusCode { get; set; }
            public required String Message { get; set; }
            public String? Details { get; set; }
        }
    }
}
