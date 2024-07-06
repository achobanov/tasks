using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Operations;
using System.Text;

namespace Challenge.Domain.Objects;

public class GameCollection : Dictionary<string, IGame>
{
    private IGame? _activeGame;
    private Notifier _notifier = new();

    public OperationsCollection Activate(string name, IFunds funds)
    {
        if (!ContainsKey(name))
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Unknown game '{name}'. The following games are available:");
            foreach (var registeredGame in Values)
            {
                sb.AppendLine($" - {registeredGame}");
            }
            _notifier.Notify(sb.ToString());
        }

        _activeGame?.Deactivate();
        var newGame = this[name];
        newGame.Activate(funds);
        _activeGame = newGame;
        return _activeGame.Operations;
    }

    public void Register(IGame game)
    {
        if (ContainsKey(game.Name))
        {
            throw new ApplicationException($"Game '{game}' is already registered");
        }
        this[game.Name] = game;
    }
}
