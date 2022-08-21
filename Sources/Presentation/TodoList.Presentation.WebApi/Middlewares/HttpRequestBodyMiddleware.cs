namespace TodoList.Presentation.WebApi.Middlewares
{
    public class HttpRequestBodyMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public HttpRequestBodyMiddleware(ILogger<HttpRequestBodyMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task Invoke(HttpContext htppContext)
        {
            htppContext.Request.EnableBuffering();

            var reader = new StreamReader(htppContext.Request.Body);
            string body = await reader.ReadToEndAsync();

            string? method = htppContext.Request?.Method;
            string? path = htppContext.Request?.Path.Value;

            _logger.LogInformation("Inicia request - Method: {method} - Path: {path} - Body: {body}", method, path, body);

            htppContext.Request.Body.Position = 0L;

            await _next(htppContext);
        }
    }
}
