using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Objects;

public readonly record struct BetResult : IBetResult
{
    private readonly decimal _abs;

    public BetResult(decimal delta, string message)
    {
        if (delta == 0)
        {
            throw new ApplicationException("Delta cannot be zero");
        }
        _abs = Math.Abs(delta);
        Delta = delta;
        Message = string.Format(message, _abs);
    }

    public decimal Delta { get; }
    public string Message { get; }

    public override string ToString()
    {
        var type = Delta > 0
            ? "Won"
            : "Lost";
        return $"{type} {_abs}";
    }
}