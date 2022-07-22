namespace TodoList.Core.Application.Interfaces.UseCases
{
    public interface IUseCase<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse?> RunAsync(TRequest request);
    }

    public interface IUseCase<TRequest>
    {
        Task RunAsync(TRequest request);
    }
}