using TodoList.Infra.Notification.Models;

namespace TodoList.Infra.Notification.Interfaces
{
    public interface INotifiable
    {
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<NotificationMessage> ErrorNotifications { get; }

        IReadOnlyList<NotificationMessage> SuccessNotifications { get; }
    }
}