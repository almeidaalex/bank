using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Bank.Domain;
using Bank.Domain.Events;
using Bank.Infra;
using MediatR;

namespace Bank.Api.Handlers
{
    public class AccountEventsHandler<TDomainEvent> : INotificationHandler<AccountEvent>
    {
        private readonly BankDbContext _context;

        public AccountEventsHandler(BankDbContext context)
        {
            _context = context;
        }
        public Task Handle(AccountEvent notification, CancellationToken cancellationToken)
        {
            CreateHistory(notification, cancellationToken);
            Debug.WriteLine(notification);
            return Task.CompletedTask;
        }

        private void CreateHistory(AccountEvent accountEvent, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.Find(accountEvent.Account.No);
            if (account is Account)
            {
                var history = new AccountHistory(accountEvent.When, accountEvent.ToString(), accountEvent.Amount, accountEvent.What);
                account.AddHistory(history);                
            }
        }
    }
}
