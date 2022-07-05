using TodoList.Infra.Notification.Abstractions;

namespace TodoList.Core.Domain.Common
{
    public abstract class BaseEntity<TId> : Notifiable 
        where TId : struct
    {
        public TId Id { get; protected set; }
    }
}