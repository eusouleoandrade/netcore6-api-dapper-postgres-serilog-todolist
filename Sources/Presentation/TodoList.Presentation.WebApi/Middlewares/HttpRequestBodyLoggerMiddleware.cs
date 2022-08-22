namespace TodoList.Presentation.WebApi.Middlewares
{
    public class HttpRequestBodyLoggerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public HttpRequestBodyLoggerMiddleware(ILogger<HttpRequestBodyLoggerMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task Invoke(HttpContext htppContext)
        {
            htppContext.Request.EnableBuffering();

            using var reader = new StreamReader(htppContext.Request.Body);

            string body = await reader.ReadToEndAsync();

            string? method = htppContext.Request?.Method;
            string? path = htppContext.Request?.Path.Value;

            // Aplicar o method, path e body do request no logger
            _logger.LogInformation("Inicia request - Method: {method} - Path: {path} - Body: {body}", method, path, body);

            if (htppContext.Request == null)
                throw new ArgumentNullException(nameof(htppContext));
            else
                htppContext.Request.Body.Position = 0L;

            await _next(htppContext);
        }
    }
}
