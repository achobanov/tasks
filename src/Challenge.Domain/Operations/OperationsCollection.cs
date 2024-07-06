using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;

namespace Challenge.Domain.Operations;

public class OperationsCollection : Dictionary<string, IOperation>
{
    public OperationsCollection()
    {
    }
    private OperationsCollection(IEnumerable<KeyValuePair<string, IOperation>> operations)
    {
        foreach (var (key, value) in operations)
        {
            Add(key, value);
        }
    }

    public void Add(IOperation operation)
    {
        Add(operation.Name, operation);
    }

    public OperationsCollection Merge(OperationsCollection collection)
    {
        var operations = this.Concat(collection);
        return new OperationsCollection(operations);
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
