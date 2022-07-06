using Microsoft.Extensions.Configuration;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Domain.Entities;

namespace TodoList.Infra.Persistence.Repositories
{
    public class TodoRepository : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        public TodoRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}