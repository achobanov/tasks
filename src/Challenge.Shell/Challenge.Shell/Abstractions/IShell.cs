namespace Challenge.Shell.Abstractions;

public interface IShell
{
    void Print(string message);
    (string command, string? arguments) ReadCommand();
}
