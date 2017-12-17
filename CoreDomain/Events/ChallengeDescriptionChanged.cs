using CoreDomain.Base;
using System;

namespace CoreDomain.Events
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
