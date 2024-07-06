using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Objects;
using Challenge.Domain.Operations.Funds;
using Challenge.Domain.Services;

namespace Challenge.Domain;

public class SlotMachine : IGame
{
    private readonly decimal _minBet;
    private readonly decimal _maxBet;
    private readonly IRandomProvider _randomProvider;
    private TresholdCollection _thresholds;
    private Notifier _notifier;
    private IFunds? _funds;

    public SlotMachine(IRandomProvider randomProvider, decimal minBet, decimal maxBet, params BetOutcome[] outcomes)
    {
        _minBet = minBet;
        _maxBet = maxBet;
        _randomProvider = randomProvider;
        _thresholds = new TresholdCollection(outcomes);
        _notifier = new Notifier();

        Operations.Add(new BetOperation(Bet));
    }

    public string Name => nameof(SlotMachine);
    public OperationsCollection Operations { get; } = [];

    public void Activate(IFunds funds)
    {
        _funds = funds;
        _notifier.Notify($"Now playing '{Name}'");
    }

    public void Deactivate()
    {
        _funds = null;
    }

    private void Bet(decimal bet)
    {
        if (_funds == null)
        {
            throw new ApplicationException($"{Name} is not active");
        }
        if (!_funds.IsAbleToBet(_minBet))
        {
            throw new DomainException($"We're sorry, but you don't have enough funds. Please deposit more using 'deposit <amount>'");
        }
        if (bet < _minBet || bet > _maxBet)
        {
            throw new DomainException($"Invalid bet '{bet}' (min: '{_minBet}', max: '{_maxBet}')");
        }

        var play = _randomProvider.GetPercent();
        var treshold = _thresholds.Match(play);
        var result = treshold.GetResult(bet);
        
        _notifier.Notify(result.Message);
        _funds.ApplyDelta(result.Delta);
    }

    public override string ToString()
    {
        return Name;
    }
}
