using Microsoft.Extensions.Logging;
using System.Text.Json;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Application.Resources;
using TodoList.Infra.Notification.Contexts;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.UseCases
{
    public class DeleteTodoUseCase : IDeleteTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<DeleteTodoUseCase> _logger;

        public DeleteTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            NotificationContext notificationContext,
            ILogger<DeleteTodoUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task RunAsync(int id)
        {
            _logger.LogInformation("Inicia o use case para remoção do todo: {id}", id);

            Validate(id);

            if (_notificationContext.HasErrorNotification)
            {
                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Erro de validação do request: {data}", data);

                return;
            }

            var todo = await _todoRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", id));

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Erro ao tentar obter o todo: {data}", data);

                return;
            }

            var removed = await _todoRepositoryAsync.RemoveAsync(id);

            if (!removed)
            {
                _notificationContext.AddErrorNotification(Msg.FALHA_AO_REMOVER_X0_COD,
                    Msg.FALHA_AO_REMOVER_X0_TXT.ToFormat("Todo"));

                var data = JsonSerializer.Serialize(_notificationContext.ErrorNotifications);
                _logger.LogWarning("Falha ao remover o todo: {data}", data);

                return;
            }

            _logger.LogInformation("Finaliza o delete use case com sucesso.");
        }

        private void Validate(int id)
        {
            if (id <= Decimal.Zero)
                _notificationContext.AddErrorNotification(Msg.IDENTIFICADOR_X0_INVÁLIDO_COD,
                    Msg.IDENTIFICADOR_X0_INVÁLIDO_TXT.ToFormat(id));
        }
    }
}
