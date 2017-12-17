using CoreDomain.Base;
using System;

namespace CoreDomain.Events
{
    public sealed class ChallengeActivated : DomainEvent
    {
        public ChallengeActivated(DateTime eventDateTime) : base(eventDateTime)
        {}
    }
}
