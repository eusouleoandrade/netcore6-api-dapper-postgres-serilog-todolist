using TodoList.Core.Application.Dtos.Models;

namespace TodoList.Core.Application.Interfaces.UseCases
{
    public interface IGetAllTodoUseCase
    {
        Task<IReadOnlyList<TodoModel>> RunAsync();
    }
}