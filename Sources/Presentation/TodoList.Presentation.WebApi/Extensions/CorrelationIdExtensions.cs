﻿using TodoList.Presentation.WebApi.Middlewares;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class CorrelationIdExtensions
    {
        public static IApplicationBuilder UseCorrelationIdExtensions(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}