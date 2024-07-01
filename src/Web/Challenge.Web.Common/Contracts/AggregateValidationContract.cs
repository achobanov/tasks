using Challenge.Domain.Core;

namespace Challenge.Web.Common.Contracts;

public class AggregateValidationContract
{
    private AggregateValidationContract()
    {
    }
    public AggregateValidationContract(DomainAggregateException aggregateValidation)
    {
        Message = aggregateValidation.Message;
        Validations = aggregateValidation.Validations
            .Select(x => x.Message)
            .ToArray();
        Count = aggregateValidation.SuccessCount;
    }

    public string Message { get; set; }
    public string[] Validations { get; set; }
    public int Count { get; set; }

    public void Throw()
    {
        var validations = Validations.Select(x => new DomainException(x));
        throw new DomainAggregateException(Message, validations);
    }
}

