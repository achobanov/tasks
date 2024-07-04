using Challenge.Domain;
using Challenge.Domain.Abstractions;
using Challenge.Domain.Chance;
using Challenge.Domain.Core;
using Challenge.Domain.Operations;
using Challenge.Shell.Abstractions;
using System.Diagnostics;
using System.Text;

namespace Challenge.Shell;

public class Interface
{
    public Dictionary<string, IGame> _games = [];

    private IGame? _selectedGame;
    private readonly IShell _shell;
    private readonly Wallet _wallet;
    private OperationsCollection _operations = [];

    public Interface(IShell shell)
    {
        _shell = shell;
        _wallet = new Wallet();
        ConfigureGames();
    }

    public void Start()
    {
        while (true)
        {
            ProcessCommand();
        }
    }

    public void ProcessCommand()
    {
        var (command, arguments) = _shell.ReadCommand();
        if (!_operations.ContainsKey(command))
        {
            RenderHelp();
            return;
        }
        _operations[command].Execute(arguments);
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

    private void SelectGame(string name)
    {
        if (!_games.ContainsKey(name))
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Unknown game '{name}'. The following games are available:");
            foreach (var game in _games.Values)
            {
                sb.AppendLine($" - {game}");
            }
            _shell.Print(sb.ToString());
        }
        
        _selectedGame?.Deactivate();
        _selectedGame = _games[name];
        _selectedGame.Activate(_wallet);
        _operations = _wallet.Operations.Merge(_selectedGame.Operations);

        _shell.Print($"Selected '{_games[name]}'");
    }

    private void ConfigureGames()
    {
        var randomProvider = new RandomProvider();
        var outcomeFactory = new BetOutcomeFactory(randomProvider);
        var loss = outcomeFactory.Loss(50);
        var win = outcomeFactory.Win(40, 1.01f, 2);
        var bigWin = outcomeFactory.Win(10, 2, 10);
        var slotMachine = new SlotMachine(randomProvider, 1, 10, loss, win, bigWin);
        _games.Add(slotMachine.Name, slotMachine);
        
        SelectGame(slotMachine.Name);
    }
}
