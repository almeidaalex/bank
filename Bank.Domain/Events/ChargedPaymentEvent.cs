using System;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
    public class ChargedPaymentEvent : IDomainEvent
    {
        private readonly decimal _amount;
        private readonly IPaybleAccount _account;
        private readonly DateTime _when;

        public ChargedPaymentEvent(Invoice invoice, IPaybleAccount account)
        {
            this._amount = invoice.Amount;
            this._account = account;
            this._when = DateTime.Now;
        }

        public IPaybleAccount Account => _account;

        public EventType What => EventType.Payment;

        public decimal Amount => _amount;

        public DateTime When => _when;
    }
}
