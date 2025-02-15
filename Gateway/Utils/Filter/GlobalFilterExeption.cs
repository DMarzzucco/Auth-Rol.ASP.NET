using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Gateway.Utils.Filter
{
    public class GlobalFilterExeption : IExceptionFilter
    {
        private readonly ILogger<GlobalFilterExeption> _logger;

        public GlobalFilterExeption(ILogger<GlobalFilterExeption> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            this._logger.LogError(context.Exception, "Unhandled Exception occured");

            var stackTrace = new StackTrace(context.Exception, true);
            var frane = stackTrace.GetFrame(0);
            var fileName = frane?.GetFileName();
            var numberLine = frane?.GetFileLineNumber();

            var statusCode = context.Exception switch
            {
                KeyNotFoundException => 404,
                UnauthorizedAccessException => 401,
                _ => 500
            };

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    404 => context.Exception.Message,
                    401 => context.Exception.Message,
                    _ => context.Exception.Message
                },
                Details = statusCode == 500 ?
                    context.Exception.InnerException?.Message : null,

                FileName = fileName,
                NumberLine = numberLine
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
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
