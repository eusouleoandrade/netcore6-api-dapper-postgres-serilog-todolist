using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TodoList.Infra.Notification.Interfaces;
using TodoList.Infra.Notification.Models;

namespace TodoList.Infra.Notification.Abstractions
{
    public abstract class Notifiable : INotifiable
    {
        private List<NotificationMessage> _errorNotifications;

        private List<NotificationMessage> _successNotifications;

        [NotMapped]
        [JsonIgnore]
        public bool HasErrorNotification => _errorNotifications.Any();

        [NotMapped]
        [JsonIgnore]
        public bool HasSuccessNotification => _successNotifications.Any();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> ErrorNotifications => _errorNotifications;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> SuccessNotifications
            => _successNotifications;

        public Notifiable()
        {
            _errorNotifications = new List<NotificationMessage>();
            _successNotifications = new List<NotificationMessage>();
        }

        protected void AddErrorNotification(NotificationMessage notification)
            => _errorNotifications.Add(notification);

        protected void AddErrorNotification(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _errorNotifications.Add(new NotificationMessage(message));
        }

        protected void AddErrorNotification(IList<string> messages)
        {
            if (messages.Any())
                messages.ToList().ForEach(message => AddErrorNotification(message));
        }

        protected void AddErrorNotification(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                notifications.ToList().ForEach(notification => AddErrorNotification(notification));
        }

        protected void AddSuccessNotification(NotificationMessage notification)
            => _successNotifications.Add(notification);

        protected void AddSuccessNotification(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _successNotifications.Add(new NotificationMessage(message));
        }

        protected void AddSuccessNotification(IList<string> messages)
        {
            if (messages.Any())
                messages.ToList().ForEach(message => AddSuccessNotification(message));
        }

        protected void AddSuccessNotification(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                notifications.ToList().ForEach(notification => AddSuccessNotification(notification));
        }
    }
}