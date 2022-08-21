using System.Net;
using System.Text.Json;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Core.Application.Exceptions;
using TodoList.Core.Application.Resources;

namespace TodoList.Presentation.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                string? method = httpContext.Request?.Method;
                string? path = httpContext.Request?.Path.Value;

                _logger.LogError(error, "Finzaliza request com falha. Method: {method} - Path: {path}.", method, path);

                string message;

                switch (error)
                {
                    case AppException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = error.Message;

                        break;
                    default:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = Msg.INTERNAL_SERVER_ERROR_TXT;

                        break;
                }

                var response = new Response(succeeded: false, message);

                var serializedResponse = JsonSerializer.Serialize(response);

                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(serializedResponse);
            }
        }
    }
}
