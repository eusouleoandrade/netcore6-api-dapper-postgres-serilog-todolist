using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Application.Resources;
using TodoList.Core.Domain.Entities;
using TodoList.Infra.Notification.Contexts;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.UseCases
{
    public class DeleteTodoUseCase : IDeleteTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;

        private readonly NotificationContext _notificationContext;

        public DeleteTodoUseCase(IGenericRepositoryAsync<Todo
            , int> genericRepositoryAsync
            , NotificationContext notificationContext)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _notificationContext = notificationContext;
        }

        public async Task RunAsync(int id)
        {
            var todo = await _genericRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_NAO_ENCONTRADO_TXT.ToFormat("Todo"));

                return;
            }

            var removed = await _genericRepositoryAsync.DeleteAsync(todo);

            if (!removed)
                _notificationContext.AddErrorNotification(Msg.FALHA_AO_REMOVER_X0_COD,
                    Msg.FALHA_AO_REMOVER_X0_TXT.ToFormat("Todo"));
        }
    }
}
