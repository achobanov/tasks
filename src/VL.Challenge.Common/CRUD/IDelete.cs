using VL.Challenge.Domain;

namespace VL.Challenge.Common.CRUD;

public interface IDelete<T>
    where T : DomainEntity, IAggregateRoot
{
    Task Delete(T entity);
    Task Delete(int id);
}
