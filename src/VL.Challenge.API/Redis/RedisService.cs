using StackExchange.Redis;
using System.Text.Json;
using VL.Challenge.Common;
using VL.Challenge.Common.CRUD;
using VL.Challenge.Domain;

namespace VL.Challenge.API.Redis;

public class RedisService<T> : ICache<T>
    where T : DomainEntity, IAggregateRoot
{
    private readonly string _typeName;
    private readonly IDatabase _database;
    private readonly IRead<T> _reader;

    public RedisService(IDatabase database, IRead<T> reader)
    {
        _database = database;
        _typeName = typeof(T).Name;
        _reader = reader;
    }

    public async Task<T> Get(int id)
    {
        return await Get(id, _reader.Read);
    }

    public async Task<T> Get(int id, Func<int, Task<T?>> getter)
    {
        var result = await _database.StringGetAsync(BuildKey(id));
        if (result.IsNull)
        {
            var entity = await getter(id);
            if (entity == null)
            {
                throw new ApplicationException($"Entity '{_typeName}' with id '{id}' does not exist");
            }
            var serialized = JsonSerializer.Serialize(entity);
            await _database.StringSetAsync(BuildKey(id), serialized);
            return entity;
        }
        return JsonSerializer.Deserialize<T>(result!)!;
    }

    public async Task Invalidate(int id)
    {
        await _database.KeyDeleteAsync(BuildKey(id));
    }

    private string BuildKey(int id)
    {
        return $"{_typeName}_{id}";
    }
}
