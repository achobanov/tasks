
namespace Challenge.Domain.Operations.Funds;

public class WithdrawOperation : FundsOperation
{
    public WithdrawOperation(Action<decimal> action) : base("Withdraw", action)
    {
    }

    public override string ToString()
    {
        return $"'withdraw <amount>': Withdraw funds from your wallet";
    }
}
