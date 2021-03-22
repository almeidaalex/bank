using Bank.Domain;


namespace Bank.Api.Commands
{
    public sealed class DepositCommand : AccountCommand<Account>
    {       
        public decimal Amount { get; set; }        
    }
}
