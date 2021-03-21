using Bank.Domain.SeedWork;
using System.Collections.Generic;

namespace Bank.Domain.SeedWork
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void ClearEvents();
    }
}
