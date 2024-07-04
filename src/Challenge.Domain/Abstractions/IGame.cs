namespace Challenge.Domain.Abstractions;

public interface IGame : IOperable
{
    string Name { get; }
    void Activate(IFunds funds);
    void Deactivate();
}
