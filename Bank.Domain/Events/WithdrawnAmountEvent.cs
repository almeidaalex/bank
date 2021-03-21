using System;

namespace Bank.Domain.SeedWork
{
    public class WithdrawnAmountEvent : IDomainEvent
    {
        private readonly decimal _amount;

        public WithdrawnAmountEvent(decimal amount)
        {
            this._amount = amount;
        }

        public EventType What => EventType.Withdraw;

        public decimal Amount => _amount;

        public DateTime When => DateTime.Now;
    }
}
