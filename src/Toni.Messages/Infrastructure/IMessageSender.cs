namespace Toni.Messages.Infrastructure;

public interface IMessageSender
{
    void Send(string json);
}


