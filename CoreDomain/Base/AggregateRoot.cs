using DomainInterfaces;
using System.Collections.Generic;

namespace Domain.Core.Base
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        readonly IList<IEvent> changes = new List<IEvent>();
        protected State State { get; set; }
        public IList<IEvent> Changes => changes;
        protected void Apply(IEvent @event)
        {
            State.Mutate(@event);
            Changes.Add(@event);
        }
    }
}
