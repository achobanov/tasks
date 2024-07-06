using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Services;

namespace Challenge.Domain.Objects;

public abstract class BetOutcome
{
    protected BetOutcome(int chance)
    {
        Percent = new Percent(chance);
    }

    public Percent Percent { get; private set; }

    public abstract IBetResult ToResult(decimal bet);
}

public class LossOutcome : BetOutcome
{
    public LossOutcome(int chance) : base(chance)
    {
    }

    public override IBetResult ToResult(decimal bet)
    {
        return new BetResult(-bet, "Sorry! You just lost '{1}' Better luck next time!");
    }

    public override string ToString()
    {
        return "Loss";
    }
}

public class WinOutcome : BetOutcome
{
    private readonly IRandomProvider _randomProvider;
    private readonly float _minModifier;
    private readonly float _maxModifier;

    public WinOutcome(int chance, IRandomProvider randomProvider, float minModifier, float maxModifier) : base(chance)
    {
        _randomProvider = randomProvider;
        _minModifier = minModifier;
        _maxModifier = maxModifier;
    }

    public override IBetResult ToResult(decimal bet)
    {
        var delta = bet * GetModifier() - bet;
        return new BetResult(delta, "Congratulations! You just won '{0}'");
    }

    public override string ToString()
    {
        return $"Win";
    }

    private decimal GetModifier()
    {
        return (decimal)_randomProvider.GetFloat(_minModifier, _maxModifier);
    }
}
