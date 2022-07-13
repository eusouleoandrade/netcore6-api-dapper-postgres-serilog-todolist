using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TodoList.Core.Application.Interfaces.Repositories;

namespace TodoList.Infra.Persistence.Repositories
{
    public class GenericRepositoryAsync<TEntity, TId> : IDisposable, IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected readonly NpgsqlConnection _connection;

        public GenericRepositoryAsync(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new NpgsqlConnection(connectionString);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _connection.GetAllAsync<TEntity>();

        public virtual async Task<TEntity> GetAsync(TId id)
            => await _connection.GetAsync<TEntity>(id);

        public virtual async Task<int> InsertAsync(TEntity entity)
            => await _connection.InsertAsync(entity);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _connection?.Dispose();
        }
    }
}