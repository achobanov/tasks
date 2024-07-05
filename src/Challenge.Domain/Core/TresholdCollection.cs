using Challenge.Domain.Objects;

namespace Challenge.Domain.Core;

public class TresholdCollection : List<Treshold>
{
    private const int FULL = 100;
    private const int SPENT = 0;
    private Percent _currentThreshold = new(SPENT);

    public TresholdCollection(IEnumerable<BetOutcome> outcomes)
    {
        foreach (var outcome in outcomes)
        {
            _currentThreshold += outcome.Percent;
            var treshold = new Treshold(_currentThreshold, outcome);
            this.Add(treshold);
        }
        if (_currentThreshold != new Percent(FULL))
        {
            throw new ApplicationException($"Invalid game configuration. Tresholds must sum to exactly 100%");
        }
    }

    internal Treshold Match(Percent playValue)
    {
        return this.First(x => x.IsMet(playValue));
    }
}
