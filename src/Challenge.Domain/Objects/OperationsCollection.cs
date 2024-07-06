using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;

namespace Challenge.Domain.Objects;

public class OperationsCollection : Dictionary<string, IOperation>
{
    public OperationsCollection()
    {
    }
    internal OperationsCollection(params OperationsCollection[] collections)
    {
        var operations = collections.SelectMany(x => x);
        foreach (var (key, value) in operations)
        {
            if (ContainsKey(key))
            {
                continue;
            }
            Add(key, value);
        }
    }

    public void Add(IOperation operation)
    {
        Add(operation.Name, operation);
    }

    public void Execute(ICommand command)
    {
        if (!ContainsKey(command.Name))
        {
            throw new DomainException($"Operation not found for command '{command}'");
        }
        this[command.Name].Execute(command.Arguments);
    }
}
