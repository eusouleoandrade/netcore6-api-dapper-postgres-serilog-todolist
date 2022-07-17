using Dapper;
using Microsoft.Extensions.Configuration;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Domain.Entities;

namespace TodoList.Infra.Persistence.Repositories
{
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        public TodoRepositoryAsync(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Todo?> AddAsync(Todo entity)
        {
            string insertSql = @"INSERT INTO todo (title, done)
                                VALUES(@title, @done)
                                RETURNING id;";

            var id = await _connection.ExecuteScalarAsync<int>(insertSql,
                new
                {
                    title = entity.Title,
                    done = entity.Done
                });

            if (id > decimal.Zero)
                return await base.GetAsync(id);

            return await Task.FromResult<Todo?>(default);
        }
    }
}