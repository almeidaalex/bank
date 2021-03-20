namespace Bank.Domain.Events
{
    public enum EventType : short
    {
        None = 0,
        Withdraw = 1,
        Deposit = 2,
        Payment = 3
    }
}
