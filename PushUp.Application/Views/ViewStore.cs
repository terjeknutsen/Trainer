using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;
using StorageInterfaces;
using System.Linq;

namespace PushUp.Application.Views
{
    class ViewStore : IViewStore
    {
        readonly Dictionary<string,IViewEventHandler> eventHandlers = new Dictionary<string, IViewEventHandler>();

        public void TryAddEventHandler(IViewEventHandler eventHandler)
        {
            if (eventHandlers.ContainsKey(eventHandler.GetType().Name))
                eventHandlers.Remove(eventHandler.GetType().Name);
            eventHandlers.Add(eventHandler.GetType().Name, eventHandler);
        }

        public async Task Update(IAggregateIdentity id, IList<IEvent> changes)
        {
            foreach(var @event in changes)
            {
                foreach (var handler in eventHandlers.Values.Where(h => h.CanHandle(@event)))
                {
                    await handler.Update(id, @event);
                }
            }
        }
    }
}
