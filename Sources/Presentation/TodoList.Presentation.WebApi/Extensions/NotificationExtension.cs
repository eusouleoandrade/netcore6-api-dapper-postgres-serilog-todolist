using TodoList.Infra.Notification.Contexts;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class NotificationExtension
    {
        public static void AddNotificationExtension(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
        }
    }
}
