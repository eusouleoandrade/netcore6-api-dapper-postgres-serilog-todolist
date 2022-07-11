using Newtonsoft.Json.Converters;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class ControllersExtension
    {
        public static void AddControllerExtension(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        }
    }
}
