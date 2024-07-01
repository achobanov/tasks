namespace Challenge.Domain.Core;

public class DomainAggregateException : Exception
{
    public DomainAggregateException(int successfulOperations, IEnumerable<DomainException> validations) : base()
    {
        Message = $"Operation resulted in '{successfulOperations}' successes but raised '{validations.Count()}' validation errors";
        Validations = validations.ToArray();
    }

    public new string Message { get; private set; }
    public DomainException[] Validations { get; private set; }

    public int SuccessCount => Validations.Length;
}