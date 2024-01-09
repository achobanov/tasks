using VL.Challenge.Domain;

namespace VL.Challenge.Common.CRUD;

public interface IUpdate<T>
    where T : DomainEntity, IAggregateRoot
{
    Task<T> Update(T entity);
}
