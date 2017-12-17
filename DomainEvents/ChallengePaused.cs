using System;

namespace DomainEvents
{
    public sealed class ChallengePaused : DomainEvent
    {
        public ChallengePaused(DateTime eventDateTime) : base(eventDateTime)
        {
        }
    }
}
