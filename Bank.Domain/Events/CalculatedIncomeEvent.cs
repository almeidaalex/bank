using Bank.Domain.Contracts;
using Bank.Domain.SeedWork;

namespace Bank.Domain.Events
{
    public sealed class CalculatedIncomeEvent : AccountEvent
    {
        public CalculatedIncomeEvent(IYieldAccount account, decimal amount)
            : base(account, EventType.Income, amount)
        {
            
        }
    }
}
