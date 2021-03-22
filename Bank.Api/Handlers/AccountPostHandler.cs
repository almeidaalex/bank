using Bank.Api.Commands;
using Bank.Domain;
using Bank.Domain.Contracts;
using Bank.Infra;
using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Handlers
{
    public class AccountPostHandler<TRequest, TResponse> : 
        IRequestPostProcessor<AccountCommand<Account>, Result<Account>>,
        IRequestPostProcessor<AccountCommand<IPaybleAccount>, Result<IPaybleAccount>>
    {
        private readonly BankDbContext _context;

        public AccountPostHandler(BankDbContext context)
        {
            this._context = context;
        }
        public Task Process(AccountCommand<Account> request, Result<Account> response, CancellationToken cancellationToken)
        {
            if (response.Success)
                _context.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task Process(AccountCommand<IPaybleAccount> request, Result<IPaybleAccount> response, CancellationToken cancellationToken)
        {
            if (response.Success)
                _context.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
