using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;

namespace PushUp.Application.EventHandlers
{
    public abstract class EventHandler<T> : IViewEventHandler
    {
        public EventHandler(IRepository<T> repository)
        {
            Repository = repository;
        }
        protected IRepository<T> Repository { get; private set; }
        protected Dictionary<string, Func<IAggregateIdentity, IEvent, Task>> Actions { get; set; }
        public bool CanHandle(IEvent @event)
        {
            return Actions.ContainsKey(@event.GetType().Name);
        }

        public async Task Update(IAggregateIdentity id, IEvent @event)
        {
            await Actions[@event.GetType().Name](id, @event);
        }
    }
}
