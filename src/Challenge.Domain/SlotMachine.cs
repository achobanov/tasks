using Challenge.Domain.Abstractions;
using Challenge.Domain.Chance;
using Challenge.Domain.Core;

namespace Challenge.Domain;

public class SlotMachine : IGame
{
    private readonly decimal _minBet;
    private readonly decimal _maxBet;
    private readonly IRandomProvider _randomProvider;
    private TresholdCollection _thresholds;

    public SlotMachine(IRandomProvider randomProvider, decimal minBet, decimal maxBet, params Treshold[] tresholds)
    {
        _minBet = minBet;
        _maxBet = maxBet;
        _randomProvider = randomProvider;
        _thresholds = new TresholdCollection(tresholds);
    }

    public IBetResult Play(decimal bet)
    {
        if (bet < _minBet || bet > _maxBet)
        {
            return new InvalidBetResult($"Invalid bet '{bet}' (min: '{_minBet}', max: '{_maxBet}')");
        }

        var play = _randomProvider.GetPercent();
        var treshold = _thresholds.Match(play);
        return treshold.GetResult(bet);
    }
}
