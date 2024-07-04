namespace Challenge.Domain.Objects;

public readonly record struct Percent : IComparable<Percent>
{
    private readonly int _value;

    public Percent(int percent)
    {
        if (percent < 0 || percent > 100)
        {
            throw new ApplicationException($"Invalid % value '{percent}'");
        }
        _value = percent;
    }

    public int CompareTo(Percent other)
    {
        return _value.CompareTo(other._value);
    }

    public static Percent operator +(Percent a, Percent b)
    {
        return new Percent(a._value + b._value);
    }

    public static Percent operator -(Percent a, Percent b)
    {
        return new Percent(a._value - b._value);
    }

    public static bool operator >=(Percent a, Percent b)
    {
        return a._value >= b._value;
    }
    public static bool operator <=(Percent a, Percent b)
    {
        return a._value <= b._value;
    }

    public override string ToString()
    {
        return _value.ToString();
    }
}
