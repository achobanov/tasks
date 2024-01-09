using VL.Challenge.Domain;

namespace VL.Challenge.Common;

public interface ICache<T>
    where T : DomainEntity
{
    Task<T> Get(int id);
    Task<T> Get(int id, Func<int, Task<T?>> getter);
    Task Invalidate(int id);
}
