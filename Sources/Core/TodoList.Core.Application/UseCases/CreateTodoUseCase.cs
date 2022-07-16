using AutoMapper;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Responses;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Domain.Entities;
using TodoList.Infra.Notification.Contexts;

namespace TodoList.Core.Application.UseCases
{
    public class CreateTodoUseCase : ICreateTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;

        private readonly IMapper _mapper;

        private readonly NotificationContext _notificationContext;

        public CreateTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            IMapper mapper,
            NotificationContext notificationContext)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public async Task<CreateTodoUseCaseResponse?> RunAsync(CreateTodoUseCaseRequest request)
        {
            Validade(request);

            if (_notificationContext.HasErrorNotification)
                return await Task.FromResult<CreateTodoUseCaseResponse?>(default);

            var todo = _mapper.Map<Todo>(request);

            var todoResponse = await _todoRepositoryAsync.AddAsync(todo);

            return _mapper.Map<CreateTodoUseCaseResponse>(todoResponse);
        }

        private void Validade(CreateTodoUseCaseRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                _notificationContext.AddErrorNotification(String.Empty, "Título é requerido");
        }
    }
}
