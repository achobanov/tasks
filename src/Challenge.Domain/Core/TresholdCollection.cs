using Challenge.Domain.Objects;

namespace Challenge.Domain.Core;

public class TresholdCollection : List<Treshold>
{
    public TresholdCollection(IEnumerable<Treshold> tresholds)
    {
        var tresholdSum = tresholds.Aggregate(new Percent(0), (sum, x) => sum + x);
        if (tresholdSum != new Percent(100))
        {
            throw new ApplicationException($"Invalid PlayTreshold sum '{tresholdSum}'. Game tresholds must always sum to a 100%");
        }
        this.AddRange(tresholds.Order());
    }

    internal Treshold Match(Percent playValue)
    {
        return this.First(x => x.IsMet(playValue));
    }
}
