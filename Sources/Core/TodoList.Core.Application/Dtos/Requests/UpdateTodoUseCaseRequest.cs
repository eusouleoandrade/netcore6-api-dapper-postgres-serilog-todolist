using TodoList.Core.Application.Resources;
using TodoList.Infra.Notification.Abstractions;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.Dtos.Requests
{
    public class UpdateTodoUseCaseRequest : Notifiable
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public UpdateTodoUseCaseRequest(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;

            Validate();
        }

        private void Validate()
        {
            if (Id == Decimal.Zero)
                AddErrorNotification(Msg.IDENTIFICADOR_X0_INVÁLIDO_COD,
                Msg.IDENTIFICADOR_X0_INVÁLIDO_TXT.ToFormat(Id));

            if (String.IsNullOrWhiteSpace(Title))
                AddErrorNotification(Msg.X0_E_OBRIGATORIO_COD,
                Msg.X0_E_OBRIGATORIO_COD.ToFormat("Title"));
        }
    }
}