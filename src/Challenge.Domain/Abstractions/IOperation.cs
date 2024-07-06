namespace Challenge.Domain.Abstractions;

public interface IOperation
{
    string Name { get; }
    bool IsDefault => false;
    void Execute(string? args);
}
