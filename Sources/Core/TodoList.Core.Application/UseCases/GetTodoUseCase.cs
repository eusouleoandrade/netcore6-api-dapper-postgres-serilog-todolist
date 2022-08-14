using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TodoList.Core.Application.Dtos.Responses;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Application.Resources;
using TodoList.Core.Domain.Entities;
using TodoList.Infra.Notification.Contexts;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.UseCases
{
    public class GetTodoUseCase : IGetTodoUseCase
    {
        private readonly NotificationContext _notificationContext;
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTodoUseCase> _logger;

        public GetTodoUseCase(NotificationContext notificationContext,
            IGenericRepositoryAsync<Todo, int> genericRepositoryAsync,
            IMapper mapper,
            ILogger<GetTodoUseCase> logger)
        {
            _notificationContext = notificationContext;
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetTodoUseCaseResponse?> RunAsync(int id)
        {
            _logger.LogInformation("Inicia o use case get todo.");

            Validade(id);

            if (_notificationContext.HasErrorNotification)
            {
                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Falha de validação: {data}", data);

                return await Task.FromResult<GetTodoUseCaseResponse?>(default);
            }

            var todo = await _genericRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", id));

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Falha ao obter o todo: {data}", data);

                return await Task.FromResult<GetTodoUseCaseResponse?>(default);
            }

            var useCaseResponse = _mapper.Map<GetTodoUseCaseResponse>(todo);

            _logger.LogInformation("Finaliza o use case get todo.");

            return useCaseResponse;
        }

        private void Validade(int id)
        {
            if (id <= Decimal.Zero)
                _notificationContext.AddErrorNotification(Msg.IDENTIFICADOR_X0_INVÁLIDO_COD,
                    Msg.IDENTIFICADOR_X0_INVÁLIDO_TXT.ToFormat(id));
        }
    }
}
