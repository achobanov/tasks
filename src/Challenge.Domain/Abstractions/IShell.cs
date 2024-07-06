namespace Challenge.Domain.Abstractions;

public interface IShell : IOperable
{
    void Print(string message);
    void PrintValidation(string message);
    void PrintError(string message);
    ICommand ReadCommand();
}
