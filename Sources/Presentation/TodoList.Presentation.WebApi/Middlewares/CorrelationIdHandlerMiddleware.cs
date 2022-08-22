using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TodoList.Presentation.WebApi.Options;

namespace TodoList.Presentation.WebApi.Middlewares
{
    public class CorrelationIdHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;

        public CorrelationIdHandlerMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(_options.Header, out StringValues correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            // Aplica o correlationId na propriedade TraceIdentifier para obter em injeções httpContext
            httpContext.TraceIdentifier = correlationId;

            // Aplica o correlationId ao cabeçalho de resposta para rastreamento lado cliente
            if (_options.IncludeInResponse)
            {
                httpContext.Response.OnStarting(() =>
                {
                    httpContext.Response.Headers.Add(_options.Header, new[] { httpContext.TraceIdentifier });
                    return Task.CompletedTask;
                });
            }

            // Aplicar o correlationId no logger
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<CorrelationIdHandlerMiddleware>>();
            using (logger.BeginScope("{@CorrelationId}", correlationId))
            {
                await _next(httpContext);
            }
        }
    }
}