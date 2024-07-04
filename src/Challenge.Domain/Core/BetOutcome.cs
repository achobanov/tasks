using Challenge.Domain.Abstractions;
using Challenge.Domain.Objects;

namespace Challenge.Domain.Core;

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
        return new BetResult(-bet, "Better luck next time!");
    }

    public override string ToString()
    {
        return "Loss";
    }
}

public class WinOutcome : BetOutcome
{
    private readonly decimal _modifier;

    public WinOutcome(int chance, float modifier) : base(chance)
    {
        _modifier = (decimal)modifier;
    }

    public override IBetResult ToResult(decimal bet)
    {
        var delta = bet * _modifier - bet;
        return new BetResult(delta, "Congratulations");
    }

    public override string ToString()
    {
        return $"Win {_modifier}";
    }
}
