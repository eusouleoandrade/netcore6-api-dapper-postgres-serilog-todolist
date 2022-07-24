using AutoMapper;
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

        public GetTodoUseCase(NotificationContext notificationContext,
            IGenericRepositoryAsync<Todo, int> genericRepositoryAsync,
            IMapper mapper)
        {
            _notificationContext = notificationContext;
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<GetTodoUseCaseResponse?> RunAsync(int id)
        {
            Validade(id);

            if (_notificationContext.HasErrorNotification)
                return await Task.FromResult<GetTodoUseCaseResponse?>(default);

            var todo = await _genericRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DADOS_DO_X0_NAO_ENCONTRADO_COD,
                    Msg.DADOS_DO_X0_NAO_ENCONTRADO_TXT.ToFormat("Todo"));

                return await Task.FromResult<GetTodoUseCaseResponse?>(default);
            }

            var useCaseResponse = _mapper.Map<GetTodoUseCaseResponse>(todo);

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
