using Dapper.Contrib.Extensions;

namespace TodoList.Core.Domain.Common
{
    public abstract class BaseEntity<TId>
        where TId : struct
    {
        [Key]
        public TId Id { get; protected set; }
    }
}