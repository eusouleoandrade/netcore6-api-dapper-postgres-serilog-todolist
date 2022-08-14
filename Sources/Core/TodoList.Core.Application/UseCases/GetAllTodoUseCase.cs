using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;

namespace TodoList.Core.Application.UseCases
{
    public class GetAllTodoUseCase : IGetAllTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTodoUseCase> _logger;

        public GetAllTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            IMapper mapper,
            ILogger<GetAllTodoUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<TodoQuery>> RunAsync()
        {
            _logger.LogInformation("Incia o use case get all todo.");

            var entities = await _todoRepositoryAsync.GetAllAsync();

            _logger.LogInformation("Finaliza o use case get all com sucesso.");

            return _mapper.Map<IReadOnlyList<TodoQuery>>(entities);
        }
    }
}