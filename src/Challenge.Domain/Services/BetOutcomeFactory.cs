using Challenge.Domain.Objects;

namespace Challenge.Domain.Services;

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
        return new WinOutcome(percentToOccur, _randomProvider, minModifier, maxModifier);
    }
}