using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Operations;

public class HelpOperation : Operation
{
    private readonly Action _action;

    public HelpOperation(Action action) : base("Help")
    {
        _action = action;
    }

    public override void Execute(string? args)
    {
        _action();
    }

    public override string ToString()
    {
        return "'help' Lists available commands";
    }
}
