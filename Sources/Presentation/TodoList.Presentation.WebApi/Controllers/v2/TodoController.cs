using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Presentation.WebApi.Controllers.Common;

namespace TodoList.Presentation.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class TodoController : BaseApiController
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
            => _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            _logger.LogInformation("Inicia endpoint v2 get all todo.");

            IReadOnlyList<TodoQuery> todoQuery = new List<TodoQuery>
            {
                new TodoQuery(1, "Ir ao mercado", false),
                new TodoQuery(2, "Ir ao médico", false),
                new TodoQuery(3, "Fazer invesitimentos", false)
            };

            _logger.LogInformation("Finaliza endepoint v2 get all todo.");

            return Ok(await Task.FromResult(new Response<IReadOnlyList<TodoQuery>>(todoQuery, true)));
        }
    }
}
