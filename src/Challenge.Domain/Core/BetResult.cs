using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Core;

public readonly record struct BetResult : IBetResult
{
    public BetResult(decimal delta, string message)
    {
        if (delta == 0)
        {
            throw new ApplicationException("Delta cannot be zero");
        }
        Delta = delta;
        Message =message;
    }

    public decimal Delta { get; }
    public string Message { get; }

    public override string ToString()
    {
        var type = Delta > 0
            ? "Won"
            : "Lost";
        return $"{type} {Math.Abs(Delta)}";
    }
}