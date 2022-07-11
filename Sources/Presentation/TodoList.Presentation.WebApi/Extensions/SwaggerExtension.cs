using Microsoft.OpenApi.Models;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo List",
                    Description = "Your to-do list."
                });
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo List - V1");
                c.RoutePrefix = string.Empty;
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
        }
    }
}
