using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TodoList.Infra.Notification.Interfaces;
using TodoList.Infra.Notification.Models;

namespace TodoList.Infra.Notification.Abstractions
{
    public abstract class Notifiable : INotifiable, IDisposable
    {
        private readonly List<NotificationMessage> _errorNotifications;

        private readonly List<NotificationMessage> _successNotifications;

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
        public IReadOnlyList<NotificationMessage> SuccessNotifications => _successNotifications;

        protected Notifiable()
        {
            _errorNotifications = new List<NotificationMessage>();
            _successNotifications = new List<NotificationMessage>();
        }

        // Error notifications
        public void AddErrorNotification(NotificationMessage notification)
            => _errorNotifications.Add(notification);

        public void AddErrorNotification(string key, string message)
        {
            _errorNotifications.Add(new NotificationMessage(key, message));
        }

        public void AddErrorNotification(string key, string message, params object[] parameters)
            => _errorNotifications.Add(new NotificationMessage(key, string.Format(message, parameters)));

        public void AddErrorNotifications(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                _errorNotifications.AddRange(notifications);
        }

        public void AddErrorNotifications(params Notifiable[] objects)
        {
            foreach (Notifiable notifiable in objects)
                _errorNotifications.AddRange(notifiable.ErrorNotifications);
        }

        // Success notification
        public void AddSuccessNotification(NotificationMessage notification)
            => _successNotifications.Add(notification);

        public void AddSuccessNotification(string key, string message)
            => _successNotifications.Add(new NotificationMessage(key, message));

        public void AddSuccessNotification(string key, string message, params object[] parameters)
            => _successNotifications.Add(new NotificationMessage(key, string.Format(message, parameters)));

        public void AddSuccessNotifications(IEnumerable<NotificationMessage> notifications)
        {
            if (notifications.Any())
                _successNotifications.AddRange(notifications);
        }

        public void AddSuccessNotifications(params Notifiable[] objects)
        {
            foreach (Notifiable notifiable in objects)
                _successNotifications.AddRange(notifiable.ErrorNotifications);
        }

        // Clear notifications
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _errorNotifications.Clear();
            _successNotifications.Clear();
        }
    }
}