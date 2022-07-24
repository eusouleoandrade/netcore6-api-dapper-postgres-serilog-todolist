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

        public SetDoneTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            NotificationContext notificationContext)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _nofificationContext = notificationContext;
        }

        public async Task RunAsync(SetDoneTodoUseCaseRequest request)
        {
            if (request.HasErrorNotification)
            {
                _nofificationContext.AddErrorNotifications(request);
                return;
            }

            var todoDataBase = await _todoRepositoryAsync.GetAsync(request.Id);

            if (todoDataBase is null)
            {
                _nofificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", request.Id));
                return;
            }

            var updated = await _todoRepositoryAsync.UpdateAsync(
                new Todo(todoDataBase.Id, todoDataBase.Title, request.Done));

            if (!updated)
                _nofificationContext.AddErrorNotification(Msg.FALHA_AO_ATUALIZAR_X0_COD,
                    Msg.FALHA_AO_ATUALIZAR_X0_TXT.ToFormat("Todo"));
        }
    }
}