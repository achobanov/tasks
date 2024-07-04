namespace Challenge.Domain.Abstractions;

public interface IWallet : IOperable
{
    void Deposit(decimal amount);
    public void Withdraw(decimal amount);
}
