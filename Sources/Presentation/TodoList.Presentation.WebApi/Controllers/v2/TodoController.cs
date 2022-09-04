using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Application.Contexts;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Presentation.WebApi.Controllers.Common;

namespace TodoList.Presentation.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class TodoController : BaseApiController
    {
        private readonly ILogger<TodoController> _logger;
        private readonly CorrelationIdContext _correlationIdContext;

        public TodoController(ILogger<TodoController> logger, CorrelationIdContext correlationIdContext)
        {
            _logger = logger;
            _correlationIdContext = correlationIdContext;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            // Exemplo de uso do CorrelationIdContext
            _logger.LogInformation("Inicia endpoint v2 get all todo. Teste correlationIdContext {correlation}", _correlationIdContext.CorrelationId);

            IReadOnlyList<TodoQuery> todoQuery = new List<TodoQuery>
            {
                new TodoQuery(1, "Ir ao mercado", false),
                new TodoQuery(2, "Ir ao médico", false),
                new TodoQuery(3, "Fazer invesitimentos", false)
            };

            // Exemplo de uso do CorrelationIdContext
            _logger.LogInformation("Finaliza endepoint v2 get all todo. Teste correlationIdContext {correlation}", _correlationIdContext.CorrelationId);

            return Ok(await Task.FromResult(new Response<IReadOnlyList<TodoQuery>>(todoQuery, true)));
        }
    }
}