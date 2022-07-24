using AutoMapper;
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

        private readonly IMapper _mapper;

        private readonly NotificationContext _notificationContext;

        public UpdateTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync,
            IMapper mapper,
            NotificationContext notificationContext)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public async Task RunAsync(UpdateTodoUseCaseRequest request)
        {
            if (request.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(request);
                return;
            }

            var todoDataBase = await _todoRepositoryAsync.GetAsync(request.Id);

            if (todoDataBase is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_X1_NAO_ENCONTRADO_TXT.ToFormat("Todo", request.Id));
                return;
            }
            
            var updated = await _todoRepositoryAsync.UpdateAsync(new
                Todo(request.Id, request.Title, request.Done));

            if (!updated)
                _notificationContext.AddErrorNotification(Msg.FALHA_AO_ATUALIZAR_X0_COD,
                    Msg.FALHA_AO_ATUALIZAR_X0_TXT.ToFormat("Todo"));
        }
    }
}