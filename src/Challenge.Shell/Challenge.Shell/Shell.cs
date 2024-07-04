using Challenge.Domain;
using Challenge.Shell.Abstractions;

namespace Challenge.Shell;

public class Shell : IShell, IDisposable
{
    private Guid _subscriptionId;

    public Shell()
    {
        _subscriptionId = Notifier.Event.Subscribe(Print);
    }

    public (string command, string? arguments) ReadCommand()
    {
        Console.WriteLine("What are you up to?");
        var input = Console.ReadLine();
        while (input == null || input == string.Empty)
        {
            Print("Invalid command");
            input = Console.ReadLine();
        }
        return Parse(input);
    }

    public void Print(string message)
    {
        Console.WriteLine(message);
    }

    public void Dispose()
    {
        Notifier.Event.Unsubscribe(_subscriptionId);
    }

    private (string command, string? arguments) Parse(string input)
    {
        var separator = input.IndexOf(' ');
        if (separator == -1)
        {
            return (input, null);
        }
        var command = input[..separator];
        var arguments = input[++separator..];
        return (command, arguments);
    }
}
