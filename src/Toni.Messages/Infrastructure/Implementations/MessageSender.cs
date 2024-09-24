namespace Toni.Messages.Infrastructure.Implementations;

public class MessageSender : IMessageSender
{
    public void Send(string json)
    {
        Console.WriteLine("Sending messages:");
        Console.Write(json);
    }
}
