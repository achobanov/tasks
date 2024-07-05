using Challenge.Domain;
using Challenge.Shell.Abstractions;
using Challenge.Shell.ConsoleProvider;

namespace Challenge.Shell;

public class Shell : IShell, IDisposable
{
    private Guid _subscriptionId;

    public Shell()
    {
        _subscriptionId = Notifier.Event.Subscribe(Print);
    }

    public ICommand ReadCommand()
    {
        Console.WriteLine("What are you up to?");
        var input = Console.ReadLine();
        while (input == null || input == string.Empty)
        {
            Print("Invalid command");
            input = Console.ReadLine();
        }
        return new ConsoleCommand(input);
    }

    public void Print(string message)
    {
        Console.WriteLine(message);
    }

    public void Dispose()
    {
        Notifier.Event.Unsubscribe(_subscriptionId);
    }
}
