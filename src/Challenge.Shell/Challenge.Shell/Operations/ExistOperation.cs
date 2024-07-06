using Challenge.Domain.Abstractions;

namespace Challenge.Console.Operations;

public class ExistOperation : Operation
{
    private readonly Action _action;

    public ExistOperation(Action action) : base("Exit")
    {
        _action = action;
    }

    public override void Execute(string? args)
    {
        _action();   
    }

    public override string ToString()
    {
        return "'exit' Exists the play session";
    }
}
