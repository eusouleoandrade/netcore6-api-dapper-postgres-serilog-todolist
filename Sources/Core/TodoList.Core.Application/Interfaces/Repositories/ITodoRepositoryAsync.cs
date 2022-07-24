using TodoList.Core.Domain.Entities;

namespace TodoList.Core.Application.Interfaces.Repositories
{
    public interface ITodoRepositoryAsync : IGenericRepositoryAsync<Todo, int>
    {
        Task<Todo?> AddAsync(Todo entity);

        Task<bool> RemoveAsync(int id);

        Task<bool> UpdateAsync(Todo entity);
    }
}