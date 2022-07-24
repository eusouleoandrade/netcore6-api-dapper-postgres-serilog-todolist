using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TodoList.Core.Application.Exceptions;
using TodoList.Core.Application.Interfaces.Repositories;
using TodoList.Core.Application.Resources;

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
        {
            try
            {
                return await _connection.GetAllAsync<TEntity>();
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            try
            {
                return await _connection.GetAsync<TEntity>(id);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                return await _connection.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

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