using System.Linq.Expressions;
using VL.Challenge.Domain;

namespace VL.Challenge.Common.CRUD;

public interface IRead<T>
    where T : DomainEntity, IAggregateRoot
{
    Task<T?> Read(int id);
    Task<IEnumerable<T>> Read();
    Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate);
}
