namespace Challenge.Domain.Abstractions;

public interface IBetResult
{
    decimal Delta { get; }
    string Message { get; }
}
