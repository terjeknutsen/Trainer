using StorageInterfaces;
using System.Collections.Generic;
using DomainInterfaces;

namespace Repositories
{
    public sealed class EventStore : IEventStore
    {
        readonly IDatabase database;

        public EventStore(IDatabase database)
        {
            this.database = database;
        }
        public void Add(IAggregateIdentity id, IList<IEvent> changes)
        {
            using (var connection = database.Connection)
            {
                connection.Add(id, changes);
            }
        }

        public IEnumerable<IEvent> Get(IAggregateIdentity id)
        {
            using(var connection = database.Connection)
            {
                return connection.Get(id);
            }
        }
    }
}
