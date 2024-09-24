namespace Toni.Messages.Infrastructure;

public class MessageReceiver
{
    ISerializer _serializer;

    public MessageReceiver(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public void Receive(string json)
    {
        var messages = _serializer.Deserialize(json);
        // TODO: add messages
    }

    public void Submit(string occasion)
    {
        // TODO: send all contained messages
    }
}
