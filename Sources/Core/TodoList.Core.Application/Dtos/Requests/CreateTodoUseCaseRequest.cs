using TodoList.Infra.Notification.Abstractions;

namespace TodoList.Core.Application.Dtos.Requests
{
    public class CreateTodoUseCaseRequest : Notifiable
    {
        public string Title { get; private set; }

        public bool Done { get; private set; } = false;

        public CreateTodoUseCaseRequest(string title)
        {
            Title = title;

            Validade();
        }

        private void Validade()
        {
            if (string.IsNullOrWhiteSpace(Title))
                AddErrorNotification("Título é requerido");
        }
    }
}
