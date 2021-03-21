namespace Bank.Domain.SeedWork
{
    public interface IAccount
    {
        int No { get; }

        Owner Owner { get; }

        Result Withdraw(decimal amount);

        void AddHistory(AccountHistory accountHistory);
    }
}
