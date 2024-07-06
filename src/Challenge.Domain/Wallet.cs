using Challenge.Domain.Abstractions;
using Challenge.Domain.Core;
using Challenge.Domain.Objects;
using Challenge.Domain.Operations.Funds;

namespace Challenge.Domain;

public class Wallet : IFunds, IWallet
{
    private readonly Notifier _notifier;
    private decimal _ballance = 0;

    public OperationsCollection Operations { get; } = [];

    public Wallet()
    {
        _notifier = new Notifier();
        Operations.Add(new DepositOperation(Deposit));
        Operations.Add(new WithdrawOperation(Withdraw));
    }

    public void Deposit(decimal amount)
    {
        _ballance += amount;
        _notifier.Notify($"Successful deposit. Your current ballance is '${_ballance}'");
    }

    public void Withdraw(decimal amount)
    {
        if (amount > _ballance)
        {
            throw new DomainException($"Insufficient funds: '${_ballance}'. Cannot withdraw '{amount}'");
        }
        _ballance -= amount;
        _notifier.Notify($"Successful withdraw. Your current ballance is '{_ballance}$'");
    }

    public void ApplyDelta(decimal delta)
    {
        _ballance += delta;
        if (_ballance < 0)
        {
            var minimumAmount = Math.Abs(_ballance) + 1;
            _notifier.Notify(
                $"Unfortunatelly your ballance is now negative '${_ballance}'. " +
                $"We're sure you'll get a better luck next time, however we'll have to ask you to deposit funds" +
                $"Untill you are on a positibe ballance. Minimum amount '${minimumAmount}'");
        }
        else
        {
            _notifier.Notify($"Current ballance is '${_ballance}'");
        }
    }

    public void AddWinnings(decimal amount)
    {
        Deposit(amount);
    }

    public bool IsAbleToBet(decimal bet)
    {
        return _ballance > bet;
    }
}
