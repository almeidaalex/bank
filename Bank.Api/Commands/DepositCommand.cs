using Bank.Domain;
using MediatR;

namespace Bank.Api.Commands
{
    public sealed class DepositCommand : IRequest<Result<Account>>
    {
        public DepositCommand(decimal amount, int accountNo)
        {
            this.Amount = amount;
            this.AccountNo = accountNo;
        }
        public decimal Amount { get; }
        public int AccountNo { get; }
    }
}
