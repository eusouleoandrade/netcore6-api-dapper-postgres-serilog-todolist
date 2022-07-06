using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.Interfaces.Repositories
{
    public interface ITodoRepositoryAsync : IGenericRepositoryAsync<Todo, int>
    {
    }
}