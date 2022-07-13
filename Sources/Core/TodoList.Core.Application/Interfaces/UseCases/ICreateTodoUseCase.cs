using TodoList.Core.Application.Dtos.Requests;
using TodoList.Core.Application.Dtos.Responses;

namespace TodoList.Core.Application.Interfaces.UseCases
{
    public interface ICreateTodoUseCase : IUseCase<CreateTodoUseCaseRequest, CreateTodoUseCaseResponse>
    {
    }
}
