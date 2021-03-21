using Bank.Domain;
using Bank.Domain.SeedWork;
using MediatR;

namespace Bank.Api.Commands
{
    public sealed class PaymentCommand : IRequest<Result<IPaybleAccount>>
    {
        public PaymentCommand(int accounNo, Invoice invoice)
        {
            this.AccountNo = accounNo;
            this.Invoice = invoice;
        }

        public int AccountNo { get; }
        public Invoice Invoice { get; }

    }
}
