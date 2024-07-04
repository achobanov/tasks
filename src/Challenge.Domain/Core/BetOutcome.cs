using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Core;

public abstract class BetOutcome
{
    public abstract IBetResult ToResult(decimal bet);
}

public class LossOutcome : BetOutcome
{
    public override IBetResult ToResult(decimal bet)
    {
        return new BetResult(-bet, "Better luck next time!");
    }
}

public class WinOutcome : BetOutcome
{
    private readonly decimal _modifier;

    public WinOutcome(float modifier)
    {
        _modifier = (decimal)modifier;
    }

    public override IBetResult ToResult(decimal bet)
    {
        var delta = bet * _modifier - bet;
        return new BetResult(delta, "Congratulations");
    }
}
