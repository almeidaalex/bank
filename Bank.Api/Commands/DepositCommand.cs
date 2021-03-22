using Bank.Domain;


namespace Bank.Api.Commands
{
    public sealed class DepositCommand : AccountCommand<Account>
    {
        public DepositCommand(decimal amount, int accountNo)
            :base(accountNo)
        {
            this.Amount = amount;
            
        }
        public decimal Amount { get; }        
    }
}
