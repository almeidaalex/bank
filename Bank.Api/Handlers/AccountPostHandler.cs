using Bank.Domain;
using Bank.Infra;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Handlers
{
    public class AccountPostHandler : IRequestPostProcessor<WithdrawCommand, Result<Account>>
    {
        private readonly BankDbContext _context;

        public AccountPostHandler(BankDbContext context)
        {
            this._context = context;
        }
        public Task Process(WithdrawCommand request, Result<Account> response, CancellationToken cancellationToken)
        {
            if (response.Success)
                _context.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
