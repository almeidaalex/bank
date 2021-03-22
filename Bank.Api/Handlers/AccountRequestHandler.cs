using Bank.Api.Commands;
using Bank.Domain;
using Bank.Domain.Contracts;
using Bank.Infra;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Handlers
{
    public class AccountHandler :
        IRequestHandler<WithdrawCommand, Result<Account>>,
        IRequestHandler<DepositCommand, Result<Account>>,
        IRequestHandler<PaymentCommand, Result<IPaybleAccount>>
    {
        private readonly BankDbContext _context;
        private readonly IPaymentService _paymentService;

        public AccountHandler(BankDbContext context, IPaymentService paymentService)
        {
            this._context = context;
            _paymentService = paymentService;
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

        public Task<Result<IPaybleAccount>> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            var account = this._context.Accounts.Find(request.AccountNo);
            if (account is IPaybleAccount payable)
            {
                var result = _paymentService.Pay(payable, request.Invoice);
                return Task.FromResult(Result.From(payable, result));
            }
            return Task.FromResult(Result.Fail<IPaybleAccount>());
        }
    }
}
