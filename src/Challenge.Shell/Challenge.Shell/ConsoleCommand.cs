using Challenge.Domain.Abstractions;

namespace Challenge.Console;

public class ConsoleCommand : ICommand
{
    public ConsoleCommand(string input)
    {
        var separator = input.IndexOf(' ');
        if (separator == -1)
        {
            Name = input;
        }
        else
        {
            Name = input[..separator];
            Arguments = input[++separator..];
        }
    }

    public string Name { get; }
    public string? Arguments { get; }
}
