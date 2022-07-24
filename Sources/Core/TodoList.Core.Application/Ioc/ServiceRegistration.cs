using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoList.Core.Application.Interfaces.UseCases;
using TodoList.Core.Application.UseCases;

namespace TodoList.Core.Application.Ioc
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IGetAllTodoUseCase, GetAllTodoUseCase>();
            services.AddScoped<ICreateTodoUseCase, CreateTodoUseCase>();
            services.AddScoped<IDeleteTodoUseCase, DeleteTodoUseCase>();
            services.AddScoped<IGetTodoUseCase, GetTodoUseCase>();
            services.AddScoped<IUpdateTodoUseCase, UpdateTodoUseCase>();
            services.AddScoped<ISetDoneTodoUseCase, SetDoneTodoUseCase>();
        }
    }
}
