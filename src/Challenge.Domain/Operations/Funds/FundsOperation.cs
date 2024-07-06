using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using System.Globalization;

namespace Challenge.Domain.Operations.Funds;

public class FundsOperation : Operation
{
    private readonly Action<decimal> _action;

    public FundsOperation(string name, Action<decimal> action) : base(name)
    {
        _action = action;
    }

    public override void Execute(string? args)
    {
        if (_action == null)
        {
            throw new DomainException($"Amount cannot be null for '{Name}'. Please provide a valid argument");
        }
        if (!decimal.TryParse(args, CultureInfo.InvariantCulture, out var decimalValue))
        {
            throw new DomainException($"Invalid amount '{args}' for '{Name}.'");
        }
        _action(decimalValue);
    }
}
