using TodoList.Infra.Notification.Contexts;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class NotificationContextExtension
    {
        public static void AddNotificationContextExtension(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
        }
    }
}
