using System;

namespace DomainEvents
{
    public sealed class ChallengeCreated : DomainEvent
    {
        public ChallengeCreated(ChallengeId id, DateTime eventDateTime) : base(eventDateTime)
        {
            Id = id;
        }

        public ChallengeId Id { get; }
    }
}
