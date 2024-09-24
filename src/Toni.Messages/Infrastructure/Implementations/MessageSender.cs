namespace Toni.Messages.Infrastructure.Implementations;

public class MessageSender : IMessageSender
{
    ISerializer _serializer;

    public MessageSender(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public void Send(string json)
    {
        var messages = _serializer.Deserialize(json);
        Console.WriteLine("Sending messages:");
        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
    }
}
