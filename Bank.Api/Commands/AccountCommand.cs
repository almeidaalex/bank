using Bank.Domain;
using MediatR;

namespace Bank.Api.Commands
{
    public abstract class AccountCommand<TAccount> : IRequest<Result<TAccount>>
    {
        public AccountCommand(int accountNo)
        {
            AccountNo = accountNo;
        }
        public int AccountNo { get; }
    }
}
