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

        public DeleteTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            NotificationContext notificationContext)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _notificationContext = notificationContext;
        }

        public async Task RunAsync(int id)
        {
            Validate(id);

            if (_notificationContext.HasErrorNotification)
                return;

            var todo = await _todoRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_NAO_ENCONTRADO_TXT.ToFormat("Todo"));

                return;
            }

            var removed = await _todoRepositoryAsync.RemoveAsync(id);

            if (!removed)
                _notificationContext.AddErrorNotification(Msg.FALHA_AO_REMOVER_X0_COD,
                    Msg.FALHA_AO_REMOVER_X0_TXT.ToFormat("Todo"));
        }

        private void Validate(int id)
        {
            if (id <= Decimal.Zero)
                _notificationContext.AddErrorNotification(Msg.IDENTIFICADOR_X0_INVÁLIDO_COD,
                    Msg.IDENTIFICADOR_X0_INVÁLIDO_TXT.ToFormat(id));
        }
    }
}
