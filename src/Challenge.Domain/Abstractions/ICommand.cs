namespace Challenge.Domain.Abstractions;

public interface ICommand
{
    string Name { get; }
    /// <summary>
    /// Space-separated arguments
    /// </summary>
    string? Arguments { get; }
}
