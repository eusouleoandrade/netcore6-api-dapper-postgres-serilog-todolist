using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TodoList.Core.Application.Exceptions;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Resources;
using TodoList.Core.Domain.Entities;

namespace TodoList.Infra.Persistence.Repositories
{
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        private readonly ILogger<TodoRepositoryAsync> _logger;

        public TodoRepositoryAsync(IConfiguration configuration, ILogger<TodoRepositoryAsync> logger)
            : base(configuration, logger)
        {
            _logger = logger;
        }

        public async Task<Todo?> AddAsync(Todo entity)
        {
            try
            {
                var data = JsonSerializer.Serialize(entity);
                _logger.LogInformation("Inicia o repositório inserir todo: {data}", data);

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

                _logger.LogInformation("Finaliza repositório com sucesso o inserir todo.");

                return await Task.FromResult<Todo?>(default);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Finaliza repositório com falha o inserir todo.");

                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                _logger.LogInformation("Inicia o repositório para remover todo: {id}", id);

                string deleteSql = @"DELETE FROM todo
                                    WHERE id = @id";

                var affectedrows = await _connection.ExecuteAsync(deleteSql, new
                {
                    id
                });

                _logger.LogInformation("Finliza repositório com sucesso o remover todo.");

                return affectedrows > Decimal.Zero;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Finaliza repositório com falha o remover todo.");

                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public async Task<bool> UpdateAsync(Todo entity)
        {
            try
            {
                var data = JsonSerializer.Serialize(entity);
                _logger.LogInformation("Inicia o repositório para atualizar todo: {data}", data);

                string updateSql = @"UPDATE todo
                                    SET title=@title, done=@done
                                    WHERE id=@id";

                var affectedrows = await _connection.ExecuteAsync(updateSql, new
                {
                    id = entity.Id,
                    title = entity.Title,
                    done = entity.Done
                });

                _logger.LogInformation("Finaliza repostório com sucesso para atualizar todo.");

                return affectedrows > Decimal.Zero;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Finaliza repositório com falha para atualizar todo.");

                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}