using Challenge.Domain.Objects;

namespace Challenge.Domain.Services;

public class RandomProvider : IRandomProvider
{
    private static readonly Random _random = new();

    public float GetFloat(float min, float max)
    {
        var minInteger = (int)min * 100;
        var maxInteger = (int)max * 100 + 1;
        var result = (float)_random.Next(minInteger, maxInteger);
        return result / 100;
    }

    public Percent GetPercent()
    {
        var result = _random.Next(1, 101);
        return new Percent(result);
    }
}

public interface IRandomProvider
{
    float GetFloat(float min, float max);
    Percent GetPercent();
}
