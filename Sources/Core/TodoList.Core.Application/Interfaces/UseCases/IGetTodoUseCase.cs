using TodoList.Core.Application.Dtos.Responses;

namespace TodoList.Core.Application.Interfaces.UseCases
{
    public interface IGetTodoUseCase : IUseCase<int, GetTodoUseCaseResponse>
    {
    }
}
