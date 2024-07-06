namespace Challenge.Domain.Abstractions;

public abstract class Operation : IOperation
{
    protected Operation(string name)
    {
        Name = name.ToLower();
    }

    public string Name { get; }
    public abstract void Execute(string? args);

    public override string ToString()
    {
        throw new NotImplementedException("Operations must override ToString");
    }
}
