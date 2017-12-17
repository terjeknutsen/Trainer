using DomainInterfaces;
using System.Collections.Generic;

namespace StorageInterfaces
{
    public interface IEventStore
    {
        IEnumerable<IEvent> Get(IAggregateIdentity id);
        void Add(IAggregateIdentity id, IList<IEvent> changes);
    }
}
