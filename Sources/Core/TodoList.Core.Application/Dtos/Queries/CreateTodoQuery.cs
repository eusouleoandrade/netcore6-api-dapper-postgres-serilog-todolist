﻿namespace TodoList.Core.Application.Dtos.Queries
{
    public class CreateTodoQuery
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public CreateTodoQuery(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}
