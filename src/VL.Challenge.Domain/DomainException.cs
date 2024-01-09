namespace VL.Challenge.Domain;

public class DomainException : ApplicationException
{
    public DomainException(string message) : base(message)
    {
    }
}
