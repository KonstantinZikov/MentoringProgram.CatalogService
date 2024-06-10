using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity<T> : EventEntity
{
    public T? Id { get; set; }
}
