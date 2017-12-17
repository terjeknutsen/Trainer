using ApplicationInterfaces;
using DomainInterfaces;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushUp.Application.Base
{
    public abstract class ServiceBase<T> : IApplicationService where T : IAggregateRoot
    {
        readonly IEventStore eventStore;

        protected ServiceBase(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }
        protected IViewStore ViewStore { private get;  set; }

        public abstract Task ExecuteCommand(ICommand cmd);
        protected abstract T BuildAggregate(IEnumerable<IEvent> events);
        protected async Task Update(IAggregateIdentity id, Action<T> execute)
        {
            var events = eventStore.Get(id);
            var aggregate = BuildAggregate(events);
            execute(aggregate);
            eventStore.Add(id, aggregate.Changes);
            await ViewStore.Update(id,aggregate.Changes);
        }
    }
}
