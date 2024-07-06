using Challenge.Console.Operations;
using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Objects;
using System.Text;

namespace Challenge.Console;

public class ConsoleShell : IShell, IDisposable
{
    private Guid _subscriptionId;

    public OperationsCollection Operations { get; } = [];

    public ConsoleShell()
    {
        _subscriptionId = Notifier.Event.Subscribe(Print);
        Operations.Add(new ExistOperation(Exit));
    }

    public ICommand ReadCommand()
    {
        System.Console.WriteLine("Type a command ('help' lists all options)");
        var input = System.Console.ReadLine();
        while (input == null || input == string.Empty)
        {
            Print("Invalid command");
            input = System.Console.ReadLine();
        }
        return new ConsoleCommand(input);
    }

    public void Print(string message)
    {
        System.Console.WriteLine(message);
    }

    public void PrintValidation(string message)
    {
        PrintColor(message, ConsoleColor.DarkYellow);
    }

    public void PrintError(string message)
    {
        PrintColor(message, ConsoleColor.DarkRed);
    }

    private void PrintColor(string message, ConsoleColor color)
    {
        System.Console.ForegroundColor = color;
        Print(message);
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public void Dispose()
    {
        Notifier.Event.Unsubscribe(_subscriptionId);
    }

    internal void Exit()
    {
        Environment.Exit(0);
    }
}
