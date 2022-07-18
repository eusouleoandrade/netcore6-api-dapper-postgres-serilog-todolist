using TodoList.Infra.Notification.Abstractions;

namespace TodoList.Infra.Notification.Contexts
{
    public class NotificationContext : Notifiable
    {
    }

    public class NotificationContext<TNotificationMessage> : Notifiable<TNotificationMessage>
        where TNotificationMessage : class
    {
    }
}
