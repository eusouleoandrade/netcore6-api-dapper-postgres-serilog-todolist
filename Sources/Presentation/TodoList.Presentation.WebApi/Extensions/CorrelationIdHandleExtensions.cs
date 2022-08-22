﻿using TodoList.Presentation.WebApi.Middlewares;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class CorrelationIdHandleExtensions
    {
        public static IApplicationBuilder UseCorrelationIdHandleExtensions(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<CorrelationIdHandlerMiddleware>();
        }
    }
}