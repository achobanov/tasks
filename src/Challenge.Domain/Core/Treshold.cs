using Challenge.Domain.Abstractions;
using Challenge.Domain.Objects;

namespace Challenge.Domain.Core;

public class Treshold : IComparable<Treshold>
{
    private readonly Percent _treshold;
    private readonly BetOutcome _betOutcome;

    public Treshold(Percent percent, BetOutcome betOutcome)
    {
        _treshold = percent;
        _betOutcome = betOutcome;
    }

    internal bool IsMet(Percent playValue)
    {
        return playValue <= _treshold;
    }

    public IBetResult GetResult(decimal bet)
    {
        return _betOutcome.ToResult(bet);
    }

    public int CompareTo(Treshold? other)
    {
        return other?._treshold.CompareTo(_treshold) ?? -1;
    }

    public static Percent operator +(Percent percent, Treshold playTreshold)
    {
        return percent + playTreshold._treshold;
    }

    public static bool operator >=(Percent percent, Treshold playTreshold)
    {
        return percent >= playTreshold._treshold;
    }

    public static bool operator <=(Percent percent, Treshold playTreshold)
    {
        return percent <= playTreshold._treshold;
    }

    public override string ToString()
    {
        return $"{_treshold}: {_betOutcome}";
    }
}
