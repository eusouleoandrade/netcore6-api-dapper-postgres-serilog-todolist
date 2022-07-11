using Microsoft.Extensions.DependencyInjection;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Infra.Persistence.Repositories;

namespace TodoList.Infra.Persistence.Ioc
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));
            services.AddScoped<ITodoRepositoryAsync, TodoRepositoryAsync>();
        }
    }
}
