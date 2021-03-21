using Bank.Domain.SeedWork;
using System;

namespace Bank.Domain.SeedWork
{
    public class DepositedAmountEvent : IDomainEvent
    {
        private readonly decimal _amount;
        private readonly IAccount _account;
        private readonly DateTime _when;

        public DepositedAmountEvent(decimal amount, IAccount account)
        {
            this._amount = amount;
            this._account = account;
            this._when = DateTime.Now; //check if needed            
        }

        public IAccount Account => _account;

        public EventType What => EventType.Deposit;

        public decimal Amount => _amount;

        public DateTime When => _when;
    }
}
