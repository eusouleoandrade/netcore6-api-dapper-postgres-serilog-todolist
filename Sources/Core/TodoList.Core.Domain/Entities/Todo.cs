using TodoList.Core.Domain.Common;

namespace TodoList.Core.Domain.Entities
{
    public class Todo : BaseEntity<int>, Notifiable
    {
        public string Title { get; private set; }

        public bool Done { get; set; }

        public Todo(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;

            Validate();
        }

        public Todo(string title, bool done) : this(default, title, done)
        {    
        }

        private void Validate()
        {
            // Validate title
            if (string.IsNullOrWhiteSpace(Title))
            {
                
            }
        }
    }
}