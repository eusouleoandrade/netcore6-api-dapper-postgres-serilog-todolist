using TodoList.Infra.Notification.Abstractions;
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

        // Error notifications
        void AddErrorNotification(NotificationMessage notification);

        void AddErrorNotification(string key, string message);

        void AddErrorNotification(string key, string message, params object[] parameters);

        void AddErrorNotifications(IEnumerable<NotificationMessage> notifications);

        void AddErrorNotifications(params Notifiable[] objects);

        // Success notification
        void AddSuccessNotification(NotificationMessage notification);

        void AddSuccessNotification(string key, string message);

        void AddSuccessNotification(string key, string message, params object[] parameters);

        void AddSuccessNotifications(IEnumerable<NotificationMessage> notifications);

        void AddSuccessNotifications(params Notifiable[] objects);
    }
}