namespace Challenge.Domain.Core;

public class DomainAggregateException : Exception
{
    public DomainAggregateException(string message, IEnumerable<DomainException> validations) : base(message)
    {
        Validations = validations.ToArray();
    }

    public DomainException[] Validations { get; }

    public int SuccessCount => Validations.Length;
}