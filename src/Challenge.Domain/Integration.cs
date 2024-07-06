using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Objects;
using Challenge.Domain.Operations;
using System.Text;

namespace Challenge.Domain;

public class Integration
{
    private readonly IShell _shell;
    private readonly Wallet _wallet;
    private OperationsCollection _operations = [];
    private GameCollection _games = [];

    public Integration(IShell shell)
    {
        _shell = shell;
        _wallet = new Wallet();
    }

    public void Start()
    {
        while (true)
        {
            SafeExecuteCommand();
        }
    }

    private void SafeExecuteCommand()
    {
        try
        {
            ExecuteCommand();
        }
        catch (DomainException validation)
        {
            _shell.Print(validation.Message);
        }
        catch (Exception)
        {
            _shell.Print($"Something went wrong. If the issue persists this action is not working as expected. Please contact support");
        }
    }

    private void ExecuteCommand()
    {
        var command = _shell.ReadCommand();
        _operations.Execute(command);
    }

    private void RenderHelp()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Command not supported. See list of available commands bellow:");
        foreach (var (command, operation) in _operations)
        {
            sb.AppendLine($" - {operation}");
        }
        _shell.Print(sb.ToString());
    }

    public void Select(string name)
    {
        var operations = _games.Activate(name, _wallet);
        _operations = _wallet.Operations.Merge(operations);
    }

    public void Register(IGame game)
    {
        _games.Register(game);
    }
}
