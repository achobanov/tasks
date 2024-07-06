using Challenge.Domain.Objects;

namespace Challenge.Domain.Abstractions;

public interface IOperable
{
    OperationsCollection Operations { get; }
}
