namespace Challenge.Domain.Abstractions;

public interface IWallet : IOperable
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}
