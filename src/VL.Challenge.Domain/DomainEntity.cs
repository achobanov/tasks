namespace VL.Challenge.Domain;

public abstract class DomainEntity : IEquatable<DomainEntity>
{
    protected DomainEntity()
    {
    }
    protected DomainEntity(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not DomainEntity entity)
        {
            return false;
        }
        return Equals(entity);
    }

    public bool Equals(DomainEntity? other)
    {
        if (other == null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
