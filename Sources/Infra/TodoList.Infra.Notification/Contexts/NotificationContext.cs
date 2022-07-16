using TodoList.Infra.Notification.Models;

namespace TodoList.Infra.Notification.Contexts
{
    public class NotificationContext
    {
        private readonly List<NotificationMessage> _errorNotifications;

        private readonly List<NotificationMessage> _successNotifications;

        public bool HasErrorNotification => _errorNotifications.Any();

        public bool HasSuccessNotification => _successNotifications.Any();

        public IReadOnlyList<NotificationMessage> ErrorNotifications
            => _errorNotifications;

        public IReadOnlyList<NotificationMessage> SuccessNotifications
            => _successNotifications;

        public NotificationContext()
        {
            _errorNotifications = new List<NotificationMessage>();
            _successNotifications = new List<NotificationMessage>();
        }

        public void AddErrorNotification(NotificationMessage notification)
            => _errorNotifications.Add(notification);

        public void AddErrorNotification(string key, string message)
        {
            _errorNotifications.Add(new NotificationMessage(key, message));
        }

        public void AddErrorNotification(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                notifications.ToList().ForEach(notification => AddErrorNotification(notification));
        }

        public void AddSuccessNotification(NotificationMessage notification)
            => _successNotifications.Add(notification);

        public void AddSuccessNotification(string key, string message)
            => _successNotifications.Add(new NotificationMessage(key, message));

        public void AddSuccessNotification(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                notifications.ToList().ForEach(notification => AddSuccessNotification(notification));
        }
    }
}
