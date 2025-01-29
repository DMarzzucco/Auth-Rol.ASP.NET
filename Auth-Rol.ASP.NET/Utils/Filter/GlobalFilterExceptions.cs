using Auth_Rol.ASP.NET.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Auth_Rol.ASP.NET.Utils.Filter
{
    public class GlobalFilterExceptions : IExceptionFilter
    {
        private readonly ILogger<GlobalFilterExceptions> _logger;

        public GlobalFilterExceptions(ILogger<GlobalFilterExceptions> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext ctx)
        {
            _logger.LogError(ctx.Exception, "Unhandled Exception occurred");

            var stackTrace = new StackTrace(ctx.Exception, true);
            var frane = stackTrace.GetFrame(0);
            var fileName = frane?.GetFileName();
            var numberLine = frane?.GetFileLineNumber();


            var statusCode = ctx.Exception switch
            {
                BadRequestException => 400,
                KeyNotFoundException => 404,
                UnauthorizedAccessException => 401,
                ConflictException => 409,
                _ => 500
            };

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    400 => ctx.Exception.Message,
                    401 => ctx.Exception.Message,
                    404 => ctx.Exception.Message,
                    409 => ctx.Exception.Message,
                    _ => ctx.Exception.Message
                },
                Details = statusCode == 500 ?
                    ctx.Exception.InnerException?.Message : null,

                FileName = fileName,
                NumberLine = numberLine
            };
            ctx.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            ctx.ExceptionHandled = true;
        }

        private class ErrorResponse
        {
            public int StatusCode { get; set; }
            public required string Message { get; set; }
            public string? Details { get; set; }
            public string? FileName { get; set; }
            public int? NumberLine { get; set; }

        }

    }
}
