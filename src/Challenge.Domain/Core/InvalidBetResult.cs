using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Core;

public class InvalidBetResult : IBetResult
{

    public InvalidBetResult(string message)
    {
        Delta = 0;
        Message = message;
    }

    public string Message { get; }

    public decimal Delta { get; }
}
