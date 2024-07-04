using Challenge.Domain.Operations;
namespace Challenge.Domain.Abstractions;

public interface IOperable
{
    OperationsCollection Operations { get; }
}
