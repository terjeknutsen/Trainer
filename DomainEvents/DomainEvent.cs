using System;

namespace DomainEvents
{
    public abstract class DomainEvent : IEvent
    {
        private readonly DateTime eventDateTime;

        protected DomainEvent(DateTime eventDateTime)
        {
            this.eventDateTime = eventDateTime;
        }
        public DateTime OccurredOn => eventDateTime;
    }
}
