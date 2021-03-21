using System;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
    public abstract class AccountEvent : IDomainEvent
    {
        public AccountEvent(IAccount account, EventType eventType, decimal amount)
        {
            this.Amount = amount;
            this.Account = account;
            this.What = eventType;
        }

        public IAccount Account { get; }

        public EventType What { get; }

        public decimal Amount { get;}

        public DateTime When => DateTime.Now;
    }
}
