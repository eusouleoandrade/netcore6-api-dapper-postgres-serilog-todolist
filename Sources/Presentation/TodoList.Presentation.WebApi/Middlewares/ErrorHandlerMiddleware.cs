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

        public ErrorHandlerMiddleware(RequestDelegate next)
            => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                var httpContextResponse = httpContext.Response;

                httpContextResponse.ContentType = "application/json";

                string message;

                switch (error)
                {
                    case AppException:
                        httpContextResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = error.Message;

                        break;
                    default:
                        httpContextResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = Msg.INTERNAL_SERVER_ERROR_TXT;

                        break;
                }

                var response = new Response(succeeded: false, message);

                var result = JsonSerializer.Serialize(response);

                await httpContextResponse.WriteAsync(result);
            }
        }
    }
}
