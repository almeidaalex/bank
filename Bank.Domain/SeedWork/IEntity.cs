using System.Collections.Generic;

using Bank.Domain.SeedWork;

namespace Bank.Domain.SeedWork
{
  public interface IEntity
  {
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent domainEvent);

    void ClearEvents();
  }
}