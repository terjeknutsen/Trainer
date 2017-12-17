using System;

namespace DomainEvents
{
    public sealed class ChallengeDescriptionChanged : DomainEvent
    {
        public ChallengeDescriptionChanged(string description,DateTime eventDateTime) : base(eventDateTime)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
