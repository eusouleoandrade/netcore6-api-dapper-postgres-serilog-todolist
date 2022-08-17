using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Infra.Notification.Contexts;
using TodoList.Presentation.WebApi.Controllers.Common;

namespace TodoList.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;
        private readonly ICreateTodoUseCase _createTodoUseCase;
        private readonly IDeleteTodoUseCase _deleteTodoUseCase;
        private readonly IGetTodoUseCase _getTodoUseCase;
        private readonly IUpdateTodoUseCase _updateTodoUseCase;
        private readonly ISetDoneTodoUseCase _setDoneTodoUseCase;
        private readonly IMapper _mapper;
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<TodoController> _logger;

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
            ICreateTodoUseCase createTodoUseCase,
            IMapper mapper,
            NotificationContext notificationContext,
            IDeleteTodoUseCase deleteTodoUseCase,
            IGetTodoUseCase getTodoUseCase,
            IUpdateTodoUseCase updateTodoUseCase,
            ISetDoneTodoUseCase setDoneTodoUseCase,
            ILogger<TodoController> logger)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _createTodoUseCase = createTodoUseCase;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _deleteTodoUseCase = deleteTodoUseCase;
            _getTodoUseCase = getTodoUseCase;
            _updateTodoUseCase = updateTodoUseCase;
            _setDoneTodoUseCase = setDoneTodoUseCase;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            _logger.LogInformation("Inicia endpoint get all todo.");

            var useCaseResponse = await _getAllTodoUseCase.RunAsync();

            _logger.LogInformation("Finaliza endepoint get all todo.");

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }

        [HttpPost]
        public async Task<ActionResult<Response<CreateTodoQuery>>> Post([FromBody] CreateTodoRequest request)
        {
            _logger.LogInformation("Inicia endpoint post todo.");

            var useCaseResponse = await _createTodoUseCase.RunAsync(_mapper.Map<CreateTodoUseCaseRequest>(request));

            if (_notificationContext.HasErrorNotification)
                return BadRequest();

            var response = _mapper.Map<CreateTodoQuery>(useCaseResponse);

            _logger.LogInformation("Finaliza endepoint post todo.");

            return Created($"/api/v1/todo/{response.Id}", new Response<CreateTodoQuery>(data: response, succeeded: true));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            _logger.LogInformation("Inicia endepoint delete todo.");

            await _deleteTodoUseCase.RunAsync(id);

            _logger.LogInformation("Finaliza endpoint delete todo.");

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetTodoQuery>>> Get(int id)
        {
            _logger.LogInformation("Inicia endepoint get todo.");

            var useCaseResponse = await _getTodoUseCase.RunAsync(id);

            var response = _mapper.Map<GetTodoQuery>(useCaseResponse);

            _logger.LogInformation("Finaliza endpoint get todo.");

            return Ok(new Response<GetTodoQuery>(succeeded: true, data: response));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Put(int id, [FromBody] UpdateTodoRequest request)
        {
            _logger.LogInformation("Inicia endpoint put todo.");

            await _updateTodoUseCase.RunAsync(new UpdateTodoUseCaseRequest(id, request.Title, request.Done));

            _logger.LogInformation("Finaliza endpoint put todo.");

            return Ok(new Response(succeeded: true));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Response>> Patch(int id, [FromBody] SetDoneTodoRequest request)
        {
            _logger.LogInformation("Inicia endpoint patch todo.");

            await _setDoneTodoUseCase.RunAsync(new SetDoneTodoUseCaseRequest(id, request.Done));

            _logger.LogInformation("Finaliza endpoint patch todo.");

            return Ok(new Response(succeeded: true));
        }
    }
}