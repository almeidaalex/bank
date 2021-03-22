using Bank.Api.DTOs;
using Bank.Domain.Contracts;

namespace Bank.Api.Commands
{
    public sealed class PaymentCommand : AccountCommand<IPaybleAccount>
    {   
        public InvoiceDto Invoice { get; set;  }

    }
}
