using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Infra.Notification.Contexts;

namespace TodoList.Presentation.WebApi.Filters
{
    public class NotificationContextFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationContextFilter(NotificationContext notificationContext)
            => _notificationContext = notificationContext;

        public async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (_notificationContext.HasErrorNotification)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.HttpContext.Response.ContentType = "application/json";

                var response = new Response(succeeded: false, errors: _notificationContext.ErrorNotifications);

                string notifications = JsonSerializer.Serialize(response);

                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
