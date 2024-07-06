using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Objects;
using Challenge.Domain.Operations;
using System.Text;

namespace Challenge.Domain;

public class Integration : IOperable
{
    private readonly IShell _shell;
    private readonly Wallet _wallet;
    private readonly GameCollection _games = [];


    public Integration(IShell shell)
    {
        _shell = shell;
        _wallet = new Wallet();
        Operations.Add(new HelpOperation(Help));
    }

    public OperationsCollection Operations { get; private set; } = [];

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
            _shell.PrintValidation(validation.Message);
        }
        catch (Exception)
        {
            _shell.PrintError($"Something went wrong. If the issue persists this action is not working as expected. Please contact support");
        }
    }

    private void ExecuteCommand()
    {
        var command = _shell.ReadCommand();
        Operations.Execute(command);
    }

    public void Activate(IGame game)
    {
        _games.Register(game);
    }

    public void Select(string name)
    {
        var gameOperations = _games.Activate(name, _wallet);
        Operations = new OperationsCollection(Operations, _wallet.Operations, _shell.Operations, gameOperations);
    }

    private void Help()
    {
        var sb = new StringBuilder();
        sb.AppendLine("See list of available commands bellow:");
        foreach (var operation in Operations.Values)
        {
            sb.AppendLine($" - {operation}");
        }
        _shell.Print(sb.ToString());
    }
}
