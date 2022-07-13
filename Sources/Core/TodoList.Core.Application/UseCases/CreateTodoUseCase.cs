using AutoMapper;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Responses;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.UseCases
{
    public class CreateTodoUseCase : ICreateTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;

        private readonly IMapper _mapper;

        public CreateTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync, IMapper mapper)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<CreateTodoUseCaseResponse?> RunAsync(CreateTodoUseCaseRequest request)
        {
            if (request.HasErrorNotification)
                return await Task.FromResult<CreateTodoUseCaseResponse?>(default);

            var todo = _mapper.Map<Todo>(request);

            var todoResponse = await _todoRepositoryAsync.AddAsync(todo);

            return _mapper.Map<CreateTodoUseCaseResponse>(todoResponse);
        }
    }
}
