using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using TodoList.Core.Application.Dtos.Wrappers;
using TodoList.Infra.Notification.Contexts;

namespace TodoList.Presentation.WebApi.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
            => _notificationContext = notificationContext;

        public async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (_notificationContext.HasErrorNotification)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var response = new Response(succeeded: false, errors: _notificationContext.ErrorNotifications);

                var notifications = JsonConvert.SerializeObject(response);

                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
