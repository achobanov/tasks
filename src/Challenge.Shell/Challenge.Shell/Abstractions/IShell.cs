namespace Challenge.Shell.Abstractions;

public interface IShell
{
    void Print(string message);
    ICommand ReadCommand();
}
