using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Presentation.WebApi.Controllers.Common;

namespace TodoList.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;

        private readonly ICreateTodoUseCase _createTodoUseCase;

        private readonly IMapper _mapper;

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
            ICreateTodoUseCase createTodoUseCase,
            IMapper mapper)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _createTodoUseCase = createTodoUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            var useCaseResponse = await _getAllTodoUseCase.RunAsync();

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }

        [HttpPost]
        public async Task<ActionResult<Response<CreateTodoQuery, List<String>>>> Post([FromBody] CreateTodoRequest request)
        {
            var useCaseRequest = _mapper.Map<CreateTodoUseCaseRequest>(request);
            var useCaseResponse = await _createTodoUseCase.RunAsync(useCaseRequest);
            var response = _mapper.Map<CreateTodoQuery>(useCaseResponse);

            return Created($"/api/v1/todo/{response.Id}", new Response<CreateTodoQuery>(response, true));
        }
    }
}