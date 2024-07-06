
namespace Challenge.Domain.Operations.Funds;

public class DepositOperation : FundsOperation
{
    public DepositOperation(Action<decimal> action) : base("Deposit", action)
    {
    }

    public override string ToString()
    {
        return $"'deposit <amount>' Deposit funds to your walltet";
    }
}
