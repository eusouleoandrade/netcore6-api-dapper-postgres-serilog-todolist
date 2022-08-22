using TodoList.Presentation.WebApi.Filters;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class ControllersExtension
    {
        public static void AddControllerExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddControllers(options => options.Filters.Add<NotificationContextFilter>());
        }
    }
}
