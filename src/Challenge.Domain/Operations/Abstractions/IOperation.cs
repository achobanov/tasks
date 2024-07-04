namespace Challenge.Domain.Operations.Abstractions;

public interface IOperation
{
    string Name { get; }
    void Execute(string? args);
}
