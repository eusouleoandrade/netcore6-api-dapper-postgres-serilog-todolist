﻿using Newtonsoft.Json.Converters;
using TodoList.Presentation.WebApi.Filters;

namespace TodoList.Presentation.WebApi.Extensions
{
    public static class ControllersExtension
    {
        public static void AddControllerExtension(this IServiceCollection services)
        {
            services
                .AddControllers(options => options.Filters.Add<NotificationFilter>())
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        }
    }
}