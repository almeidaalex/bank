using Bank.Domain;
using MediatR;

namespace Bank.Api
{
    public sealed class WithdrawCommand : IRequest<Result<Account>>
    {
        public WithdrawCommand(decimal amount, int accountNo)
        {
            this.Amount = amount;
            this.AccountNo = accountNo;
        }
        public decimal Amount { get; }
        public int AccountNo { get; }
    }
}
