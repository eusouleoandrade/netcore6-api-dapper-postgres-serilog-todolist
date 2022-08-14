using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
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
        private readonly ILogger<CreateTodoUseCase> _logger;

        public CreateTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            IMapper mapper,
            NotificationContext notificationContext,
            ILogger<CreateTodoUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task<CreateTodoUseCaseResponse?> RunAsync(CreateTodoUseCaseRequest request)
        {
            _logger.LogInformation("Inicia use case para criar todo.");

            if (request.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(request);

                _logger.LogWarning($"Erros de validações do request: {JsonSerializer.Serialize(request.ErrorNotifications)}");

                return await Task.FromResult<CreateTodoUseCaseResponse?>(default);
            }

            var todo = _mapper.Map<Todo>(request);

            var todoResponse = await _todoRepositoryAsync.AddAsync(todo);

            _logger.LogInformation("Finaliza com sucesso o use case para criar todo.");

            return _mapper.Map<CreateTodoUseCaseResponse>(todoResponse);
        }
    }
}
