using Challenge.Domain.Chance;

namespace Challenge.Domain.Core;

public class BetOutcomeFactory
{
    private readonly IRandomProvider _randomProvider;

    public BetOutcomeFactory(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public LossOutcome Loss(int percentToOccur)
    {
        return new LossOutcome(percentToOccur);
    }

    public WinOutcome Win(int percentToOccur, float minModifier, float maxModifier)
    {
        var modifier = _randomProvider.GetFloat(minModifier, maxModifier);
        return new WinOutcome(percentToOccur, modifier);
    }
}
