using Bank.Domain.Contracts;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
  public sealed class DepositedAmountEvent : AccountEvent
  {
    public DepositedAmountEvent(decimal amount, IAccount account)
        : base(account, EventType.Deposit, amount)
    {

    }

    public override string ToString() =>
        $"Depósito realizado no valor de {Amount:c2}";
  }
}