using Microsoft.AspNetCore.Mvc;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class VersioningExtension
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
