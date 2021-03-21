using System.Collections.Generic;

namespace Bank.Domain.SeedWork
{
    public abstract class Entity : IEntity
    {
        private readonly List<IDomainEvent> _events;

        public Entity()
        {
            _events = new List<IDomainEvent>();
        }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public void AddDomainEvent(IDomainEvent domainEvent) =>
            _events.Add(domainEvent);

        public void ClearEvents() =>
            _events.Clear();
    }
}
