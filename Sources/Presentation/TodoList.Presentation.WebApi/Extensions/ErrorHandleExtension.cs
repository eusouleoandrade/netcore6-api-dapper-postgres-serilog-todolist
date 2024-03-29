﻿using TodoList.Presentation.WebApi.Middlewares;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class ErrorHandleExtension
    {
        public static void UseErrorHandlingExtension(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}