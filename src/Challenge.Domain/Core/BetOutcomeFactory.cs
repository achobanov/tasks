using Challenge.Domain.Chance;

namespace Challenge.Domain.Core;

public class BetOutcomeFactory
{
    private readonly IRandomProvider _randomProvider;

    public BetOutcomeFactory(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public LossOutcome Loss()
    {
        return new LossOutcome();
    }

    public WinOutcome Win(int minModifier, int maxModifier)
    {
        var modifier = _randomProvider.GetFloat(minModifier, maxModifier);
        return new WinOutcome(modifier);
    }
}
