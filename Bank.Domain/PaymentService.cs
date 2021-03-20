using Bank.Domain.Contracts;

namespace Bank.Domain
{
    public class PaymentService
    {
        public Result Pay(IAccount account, decimal amount)
        {   
             return account.Withdraw(amount);
        }
    }
}
