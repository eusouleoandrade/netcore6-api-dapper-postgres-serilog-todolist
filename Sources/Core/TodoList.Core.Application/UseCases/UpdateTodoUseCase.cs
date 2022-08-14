using Microsoft.Extensions.Logging;
using System.Text.Json;
using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Application.Resources;
using TodoList.Core.Domain.Entities;
using TodoList.Infra.Notification.Contexts;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.UseCases
{
    public class UpdateTodoUseCase : IUpdateTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<UpdateTodoUseCase> _logger;

        public UpdateTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            NotificationContext notificationContext,
            ILogger<UpdateTodoUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task RunAsync(UpdateTodoUseCaseRequest request)
        {
            _logger.LogInformation("Inicia o use case update todo");

            if (request.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(request);

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Erro de validação do request: {data}", data);

                return;
            }

            var todoDataBase = await _todoRepositoryAsync.GetAsync(request.Id);

            if (todoDataBase is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", request.Id));

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Erro ao obter o todo: {data}", data);

                return;
            }

            var updated = await _todoRepositoryAsync.UpdateAsync(new Todo(request.Id, request.Title, request.Done));

            if (!updated)
            {
                _notificationContext.AddErrorNotification(Msg.FALHA_AO_ATUALIZAR_X0_COD,
                    Msg.FALHA_AO_ATUALIZAR_X0_TXT.ToFormat("Todo"));

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Erro ao atualizar todo: {data}", data);

                return;
            }

            _logger.LogInformation("Finaliza o use case update todo com sucesso.");
        }
    }
}