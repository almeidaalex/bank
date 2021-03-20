using System;

namespace Bank.Domain.Events
{
    public class WithdrawnAmount : IDomainEvent
    {
        private readonly decimal _amount;

        public WithdrawnAmount(decimal amount)
        {
            this._amount = amount;
        }

        public EventType What => EventType.Withdraw;

        public decimal Amount => _amount;

        public DateTime When => DateTime.Now;
    }
}
