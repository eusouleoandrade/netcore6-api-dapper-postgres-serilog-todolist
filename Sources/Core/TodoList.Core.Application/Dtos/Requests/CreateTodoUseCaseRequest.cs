using TodoList.Core.Application.Resources;
using TodoList.Infra.Notification.Abstractions;
using TodoList.Infra.Notification.Extensions;

namespace TodoList.Core.Application.Dtos.Requests
{
    public class CreateTodoUseCaseRequest : Notifiable
    {
        public string Title { get; private set; }

        public bool Done { get; private set; } = false;

        public CreateTodoUseCaseRequest(string title)
        {
            Title = title;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                AddErrorNotification(Msg.X0_E_OBRIGATORIO_COD, Msg.X0_E_OBRIGATORIO_TXT.ToFormat("Title"));
        }
    }
}
