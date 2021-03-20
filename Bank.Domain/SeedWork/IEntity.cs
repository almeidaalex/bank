using Bank.Domain.Events;
using System.Collections.Generic;

namespace Bank.Domain.Contracts
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void ClearEvents();
    }
}
