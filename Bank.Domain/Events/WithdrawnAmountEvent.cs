using Bank.Domain.Contracts;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
  public sealed class WithdrawnAmountEvent : AccountEvent
  {
    public WithdrawnAmountEvent(IAccount account, decimal amount)
        : base(account, EventType.Withdraw, amount * -1)
    {

    }
    public override string ToString() =>
        $"Saque realizado no valor de {Amount:c2}";
  }
}