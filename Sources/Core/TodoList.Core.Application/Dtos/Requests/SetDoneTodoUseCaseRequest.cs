using TodoList.Core.Application.Resources;
using TodoList.Infra.Notification.Abstractions;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.Dtos.Requests
{
    public class SetDoneTodoUseCaseRequest : Notifiable
    {
        public int Id { get; private set; }

        public bool Done { get; private set; }

        public SetDoneTodoUseCaseRequest(int id, bool done)
        {
            Id = id;
            Done = done;

            Validate();
        }

        private void Validate()
        {
            if (Id <= Decimal.Zero)
                AddErrorNotification(Msg.IDENTIFICADOR_X0_INVÁLIDO_COD,
                Msg.IDENTIFICADOR_X0_INVÁLIDO_TXT.ToFormat(Id));
        }
    }
}