using TodoList.Core.Application.Dtos.Queries;

namespace TodoList.Core.Application.Interfaces.UseCases
{
    public interface IGetAllTodoUseCase
    {
        Task<IReadOnlyList<TodoQuery>> RunAsync();
    }
}