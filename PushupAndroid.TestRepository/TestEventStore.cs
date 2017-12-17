using System.Collections.Generic;
using DomainInterfaces;
using StorageInterfaces;

namespace PushupAndroid.TestRepository
{
    public sealed class TestEventStore : IEventStore
    {
        Dictionary<string, List<IEvent>> events = new Dictionary<string, List<IEvent>>();
        public void Add(IAggregateIdentity id, IList<IEvent> changes)
        {
            if (events.ContainsKey(id.ToString()))
                events[id.ToString()].AddRange(changes);
            else
                events.Add(id.ToString(), changes as List<IEvent>);
        }

        public IEnumerable<IEvent> Get(IAggregateIdentity id)
        {
            if (events.ContainsKey(id.ToString()))
                return events[id.ToString()];
            return new List<IEvent>();
        }
    }
}