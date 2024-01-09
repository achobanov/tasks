using VL.Challenge.Domain;

namespace VL.Challenge.Common.CRUD;

public interface ICreate<T>
    where T : DomainEntity, IAggregateRoot
{
    Task<T> Create(T entity);
}
