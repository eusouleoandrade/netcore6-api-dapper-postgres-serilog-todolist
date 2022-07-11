using AutoMapper;
using TodoList.Core.Application.Dtos.Queries;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;

namespace TodoList.Core.Application.UseCases
{
    public class GetAllTodoUseCase : IGetAllTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;

        private readonly IMapper _mapper;

        public GetAllTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync, IMapper mapper)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TodoQuery>> RunAsync()
        {
            var entities = await _todoRepositoryAsync.GetAllAsync();
            return _mapper.Map<IReadOnlyList<TodoQuery>>(entities);
        }
    }
}