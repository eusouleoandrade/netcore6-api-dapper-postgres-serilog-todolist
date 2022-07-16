namespace TodoList.Core.Application.Dtos.Requests
{
    public class CreateTodoUseCaseRequest
    {
        public string Title { get; private set; }

        public bool Done { get; private set; } = false;

        public CreateTodoUseCaseRequest(string title)
        {
            Title = title;
        }
    }
}
