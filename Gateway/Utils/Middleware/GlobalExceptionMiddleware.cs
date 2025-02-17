using System.Net;
using System.Text.Json;

namespace Gateway.Utils.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode >= 400 && !context.Response.HasStarted)
                {
                    await HandleErrorResponse(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleErrorResponse(context, (int)HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task HandleErrorResponse(HttpContext context, int? statusCode = null, Exception? ex = null)
        {
            context.Response.ContentType = "application/json";

            var originalStatusCode = statusCode ?? context.Response.StatusCode;
            var responseBody = await ReadResponseBody(context);

            var response = new
            {
                statusCode = originalStatusCode,
                message = !string.IsNullOrEmpty(responseBody) ? responseBody : GetErrorMessage(originalStatusCode),
                details = ex?.StackTrace
            };

            context.Response.StatusCode = originalStatusCode;
            _logger.LogError("Error {StatusCode}: {Message}", originalStatusCode, response.message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task<string> ReadResponseBody(HttpContext context)
        {
            try
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(context.Response.Body);
                return await reader.ReadToEndAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetErrorMessage(int statusCode)
        {
            var messages = new Dictionary<int, string>
            {
                { 400, "Solicitud incorrecta" },
                { 401, "No autorizado" },
                { 403, "Prohibido" },
                { 404, "Recurso no encontrado" },
                { 409, "Conflicto en la solicitud" },
                { 500, "Error interno del servidor" }
            };

            return messages.TryGetValue(statusCode, out var message) ? message : "Error desconocido";
        }
    }
}
