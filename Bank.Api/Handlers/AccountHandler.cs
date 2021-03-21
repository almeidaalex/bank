using Bank.Api.Commands;
using Bank.Domain;
using Bank.Infra;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Handlers
{
    public class AccountHandler :
        IRequestHandler<WithdrawCommand, Result<Account>>,
        IRequestHandler<DepositCommand, Result<Account>>
    {
        private readonly BankDbContext _context;

        public AccountHandler(BankDbContext context)
        {
            this._context = context;
        }

        public Task<Result<Account>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = this._context.Accounts.Find(request.AccountNo);
            if (account is Account)
            {
                var result = account.Withdraw(request.Amount);
                return Task.FromResult(Result.From(account, result));

            }
            return Task.FromResult(Result.Fail<Account>());
        }

        public Task<Result<Account>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = this._context.Accounts.Find(request.AccountNo);
            if (account is Account)
            {
                var result = account.Deposit(request.Amount);
                return Task.FromResult(Result.From(account, result));

            }
            return Task.FromResult(Result.Fail<Account>());
        }
    }
}
