using VL.Challenge.Domain;

namespace VL.Challenge.Common.CRUD;

public interface IRepository<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T>
    where T : DomainEntity, IAggregateRoot
{
}
