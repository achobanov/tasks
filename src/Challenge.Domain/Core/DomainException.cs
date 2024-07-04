namespace Challenge.Domain.Core;

public class DomainException : ApplicationException
{
    public DomainException(string message) : base(message)
    {
    }
}
