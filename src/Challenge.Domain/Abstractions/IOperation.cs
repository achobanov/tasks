namespace Challenge.Domain.Abstractions;

public interface IOperation
{
    string Name { get; }
    void Execute(string? args);
}
