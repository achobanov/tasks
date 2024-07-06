namespace Challenge.Domain.Abstractions;

public interface IShell
{
    void Print(string message);
    ICommand ReadCommand();
}
