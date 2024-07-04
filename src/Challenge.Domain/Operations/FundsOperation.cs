using Challenge.Domain.Core;
using Challenge.Domain.Operations.Abstractions;

namespace Challenge.Domain.Operations;

public class FundsOperation : IOperation
{
    private readonly Action<decimal> _action;

    public FundsOperation(string name, Action<decimal> action)
    {
        Name = name.ToLower();
        _action = action;
    }

    public string Name { get; }

    public void Execute(string? args)
    {
        if (_action == null)
        {
            throw new DomainException($"Amount cannot be null for '{Name}'. Please provide a valid argument");
        }
        if (!decimal.TryParse(args, out var decimalValue))
        {
            throw new DomainException($"Invalid amount '{args}' for '{Name}.'");
        }
        _action(decimalValue);
    }

    public override string ToString()
    {
        return $"{Name} <amount>";
    }
}
