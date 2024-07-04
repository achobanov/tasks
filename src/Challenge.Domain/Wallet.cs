using Challenge.Common;
using Challenge.Domain.Core;

namespace Challenge.Domain;

public class Wallet
{
    private decimal _ballance = 0;

    public Event<string> NotificationEvent { get; } = new();

    public void Deposit(decimal amount)
    {
        _ballance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > _ballance)
        {
            throw new DomainException($"Insufficient funds: '{_ballance}'. Cannot withdraw '{amount}'");
        }
        _ballance -= amount;
    }

    public void AddWinnings(decimal amount)
    {
        Deposit(amount);
    }

    public void SubtractLosses(decimal amount)
    {
        _ballance -= amount;
        if (_ballance < 0)
        {
            var minimumAmount = Math.Abs(_ballance) + 1;
            NotificationEvent.Emmit(
                $"Unfortunatelly your ballance is now negative '{_ballance}'. " +
                $"We're sure you'll get a better luck next time, however we'll have to ask you to deposit funds" +
                $"Untill you are on a positibe budget. Minimum amount '{minimumAmount}'. Do you wish to deposit now?");
        }
    }

    internal bool CanBet(decimal minimumBet)
    {
        return _ballance > minimumBet;
    }
}
