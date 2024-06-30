namespace Challenge.Domain;

/// <summary>
/// DomainException is raised in case of validation errors
/// </summary>
public class DomainException : ApplicationException
{
    public DomainException(string message) : base(message)
    {
    }
}
