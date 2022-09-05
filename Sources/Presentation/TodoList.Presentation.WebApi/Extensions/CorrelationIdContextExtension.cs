using TodoList.Infra.Logger.Contexts;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class CorrelationIdContextExtension
    {
        public static void AddCorrelationIdContextExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<CorrelationIdContext>();
        }
    }
}