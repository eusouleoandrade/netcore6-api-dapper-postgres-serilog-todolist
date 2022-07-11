using Microsoft.Extensions.Configuration;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Domain.Entities;

namespace TodoList.Infra.Persistence.Repositories
{
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        public TodoRepositoryAsync(IConfiguration configuration) : base(configuration)
        {
        }
    }
}