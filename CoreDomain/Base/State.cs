using DomainInterfaces;
using System.Collections.Generic;

namespace Domain.Core.Base
{
    public abstract class State
    {
        private long next;
        protected State(IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                next++;
                Mutate(@event);
            }
        }
        public long NextEventId => next;
        protected internal void Mutate(IEvent @event)
        {
            ((dynamic)this).When((dynamic)@event);
        }

        protected internal bool Created { get; set; }
    }
}
