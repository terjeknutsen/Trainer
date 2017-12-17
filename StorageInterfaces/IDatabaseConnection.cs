using System.Collections.Generic;
using DomainInterfaces;
using System;

namespace StorageInterfaces
{
    public interface IDatabaseConnection : IDisposable
    {
        void Add(IAggregateIdentity id, IList<IEvent> changes);
        IEnumerable<IEvent> Get(IAggregateIdentity id);
    }
}