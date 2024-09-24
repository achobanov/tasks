using System.Text.Json;

namespace Toni.Messages.Infrastructure.Implementations;

public class Serializer : ISerializer
{
    public IEnumerable<object> Deserialize(string json)
    {
        var objects = JsonSerializer.Deserialize<IEnumerable<object>>(json) ?? [];
        return objects;
    }

    public string Serialize(IEnumerable<object> obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }
}
