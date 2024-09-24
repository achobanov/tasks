namespace Toni.Messages.Infrastructure;
public interface ISerializer
{
    // TODO: replace object with domain class
    IEnumerable<object> Deserialize(string json);
    string Serialize(IEnumerable<object> obj);
}
