using TodoList.Presentation.WebApi.Middlewares;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class HttpRequestHandlerExtension
    {
        public static void UseHttpRequestBodyLoggerExtension(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<HttpRequestBodyLoggerMiddleware>();
        }
    }
}