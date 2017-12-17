using System;

namespace DomainEvents
{
    public sealed class ChallengeActivated : DomainEvent
    {
        public ChallengeActivated(DateTime eventDateTime) : base(eventDateTime)
        {
        }
    }
}
