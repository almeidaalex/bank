using Bank.Domain;
using Bank.Domain.Contracts;

namespace Bank.Api.Commands
{
    public sealed class PaymentCommand : AccountCommand<IPaybleAccount>
    {
        public PaymentCommand(int accountNo, Invoice invoice)
            :base(accountNo)
        {
            
            this.Invoice = invoice;
        }        
        public Invoice Invoice { get; }

    }
}
