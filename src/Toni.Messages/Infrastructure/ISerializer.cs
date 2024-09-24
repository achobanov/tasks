namespace Toni.Messages.Infrastructure;
internal interface ISerializer
{
    // TODO: replace object with domain class
    IEnumerable<object> Deserialize(string json);
    string Serialize(IEnumerable<object> obj);
}
