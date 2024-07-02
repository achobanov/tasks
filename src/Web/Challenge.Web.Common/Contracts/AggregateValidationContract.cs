using Challenge.Domain.Core;
using Newtonsoft.Json;

namespace Challenge.Web.Common.Contracts;

public class AggregateValidationContract
{
    [JsonConstructor]
    private AggregateValidationContract(string message, string[] validations, int count)
    {
        Message = message;
        Validations = validations;
        Count = count;
    }
    public AggregateValidationContract(DomainAggregateException aggregateValidation)
    {
        Message = aggregateValidation.Message;
        Validations = aggregateValidation.Validations
            .Select(x => x.Message)
            .ToArray();
        Count = aggregateValidation.SuccessCount;
    }

    public string Message { get; }
    public string[] Validations { get; }
    public int Count { get; }

    public void Throw()
    {
        var validations = Validations.Select(x => new DomainException(x));
        throw new DomainAggregateException(Message, validations);
    }
}

