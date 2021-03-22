using Bank.Api.Commands;
using Bank.Domain;

namespace Bank.Api
{
    public sealed class WithdrawCommand : AccountCommand<Account>
    {     
        public decimal Amount { get; set; }        
    }
}
