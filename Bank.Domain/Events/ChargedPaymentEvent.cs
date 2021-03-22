using System;
using Bank.Domain.Contracts;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
    public sealed class ChargedPaymentEvent : AccountEvent
    {
        private readonly Invoice _invoice;
        public ChargedPaymentEvent(IAccount account, Invoice invoice)
            : base(account, EventType.Payment, invoice.Amount * -1)
        {
            _invoice = invoice;
        }
        public override string ToString() =>
            $"Pagamento #{_invoice.Number} realizado no valor de {Amount:c2}";
    }
}
