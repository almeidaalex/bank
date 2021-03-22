using Bank.Api.Commands;
using Bank.Domain;

namespace Bank.Api
{
    public sealed class WithdrawCommand : AccountCommand<Account>
    {
        public WithdrawCommand(decimal amount, int accountNo)
            :base(accountNo)
        {
            this.Amount = amount;            
        }
        public decimal Amount { get; }        
    }
}
