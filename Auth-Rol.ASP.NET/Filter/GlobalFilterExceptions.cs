using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth_Rol.ASP.NET.Filter
{
    public class GlobalFilterExceptions : IExceptionFilter
    {
        private readonly ILogger<GlobalFilterExceptions> _logger;

        public GlobalFilterExceptions(ILogger<GlobalFilterExceptions> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext ctx)
        {
            this._logger.LogError(ctx.Exception, "Internal Error Server");

            var statusCode = ctx.Exception is KeyNotFoundException ? 404 : 500;

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = statusCode == 500
                ? "Internal Error Server, Please try again later."
                : " Resource not found",
                Details = statusCode == 500 ? null : ctx.Exception.Message
            };
            ctx.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            ctx.ExceptionHandled = true;
        }

        public class ErrorResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string? Details { get; set; }
        }

    }
}
