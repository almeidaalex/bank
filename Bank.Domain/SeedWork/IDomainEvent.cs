using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Events
{
    public interface IDomainEvent
    {
        EventType What { get; }
        decimal Amount { get; }
        DateTime When { get; }
    }
}
