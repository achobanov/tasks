namespace Challenge.Domain.Abstractions;

public interface IFunds
{
    bool IsAbleToBet(decimal minBet);
    void ApplyDelta(decimal delta);
}
