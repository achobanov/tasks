using Challenge.Common;

namespace Challenge.Domain.Files.Objects;

public struct Filesize
{
    public Filesize(int bytesLimit)
    {
        BytesCount = bytesLimit;
    }

    public int BytesCount { get; }

    public static bool operator ==(Filesize left, Filesize right)
    {
        return left.BytesCount == right.BytesCount;
    }

    public static bool operator !=(Filesize left, Filesize right)
    {
        return left.BytesCount != right.BytesCount;
    }

    public static bool operator >(Filesize left, Filesize right)
    {
        return left.BytesCount > right.BytesCount;
    }

    public static bool operator <(Filesize left, Filesize right)
    {
        return left.BytesCount > right.BytesCount;
    }

    public static bool operator >=(Filesize left, Filesize right)
    {
        return left > right || left == right;
    }

    public static bool operator <=(Filesize left, Filesize right)
    {
        return left < right || left == right;
    }

    public override string ToString()
    {
        var mbs = BytesCount / Constants.MegaByte;
        if (mbs >= 1)
        {
            return $"{mbs:0} MB";
        }
        var kbs = BytesCount / Constants.KilloByte;
        return $"{kbs:0} KB";
    }

    public override bool Equals(object? other)
    {
        if (other is null)
        {
            return false;
        }
        if (other is Filesize otherFileLimit && BytesCount.Equals(otherFileLimit.BytesCount))
        {
            return true;
        }
        if (other is int integer && BytesCount.Equals(integer))
        {
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return BytesCount.GetHashCode();
    }
}
