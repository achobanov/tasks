using Toni.Messages.Infrastructure;

namespace Toni.Messages.Domain;

public class Messages
{
    IMessageSender _sender;
    ISerializer _serializer;

    public void Send(string occasion)
    {
        // Implementation

        var messagesToSend = _serializer.Serialize([]);
        _sender.Send(messagesToSend);
    }
}
