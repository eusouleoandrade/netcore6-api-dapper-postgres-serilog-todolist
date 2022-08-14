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
    public class SetDoneTodoUseCase : ISetDoneTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly NotificationContext _nofificationContext;
        private readonly ILogger<SetDoneTodoUseCase> _logger;

        public SetDoneTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            NotificationContext notificationContext,
            ILogger<SetDoneTodoUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _nofificationContext = notificationContext;
            _logger = logger;
        }

        public async Task RunAsync(SetDoneTodoUseCaseRequest request)
        {
            _logger.LogInformation("Inicia o use case para set done todo.");

            if (request.HasErrorNotification)
            {
                _nofificationContext.AddErrorNotifications(request);

                var data = JsonSerializer.Serialize(_nofificationContext.ErrorNotifications);
                _logger.LogWarning("Erro de validação do request: {data}", data);

                return;
            }

            var todoDataBase = await _todoRepositoryAsync.GetAsync(request.Id);

            if (todoDataBase is null)
            {
                _nofificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", request.Id));

                var data = JsonSerializer.Serialize(_nofificationContext.ErrorNotifications);
                _logger.LogWarning("Erro ao obter o todo: {data}", data);

                return;
            }

            var updated = await _todoRepositoryAsync.UpdateAsync(new Todo(todoDataBase.Id, todoDataBase.Title, request.Done));

            if (!updated)
            {
                _nofificationContext.AddErrorNotification(Msg.FALHA_AO_ATUALIZAR_X0_COD,
                    Msg.FALHA_AO_ATUALIZAR_X0_TXT.ToFormat("Todo"));

                var data = JsonSerializer.Serialize(_nofificationContext.ErrorNotifications);
                _logger.LogWarning("Falha ao atualizar todo: {data}", data);

                return;
            }

            _logger.LogInformation("Finaliza com sucesso o use case set done todo.");
        }
    }
}