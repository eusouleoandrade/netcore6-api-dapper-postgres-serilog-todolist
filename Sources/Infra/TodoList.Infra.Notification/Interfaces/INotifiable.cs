using TodoList.Infra.Notification.Models;

namespace TodoList.Infra.Notification.Interfaces
{
    public interface INotifiable
    {
        // Properties
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<NotificationMessage> ErrorNotifications { get; }

        IReadOnlyList<NotificationMessage> SuccessNotifications { get; }
    }
}