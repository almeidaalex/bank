using Bank.Api.DTOs;
using Bank.Domain;
using Bank.Domain.Contracts;

namespace Bank.Api.Commands
{
    public sealed class PaymentCommand : AccountCommand<Account>
    {   
        public InvoiceDto Invoice { get; set;  }

    }
}
