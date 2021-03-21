using System;

namespace Bank.Domain.SeedWork
{
    public interface IDomainEvent
    {
        EventType What { get; }
        decimal Amount { get; }
        DateTime When { get; }
    }
}
