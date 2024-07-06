namespace Challenge.Domain.Operations.Funds;

public class BetOperation : FundsOperation
{
    public BetOperation(Action<decimal> action) : base("Bet", action)
    {
    }

    public override string ToString()
    {
        return $"'bet <amount>' Place a bet on the active game";
    }
}
