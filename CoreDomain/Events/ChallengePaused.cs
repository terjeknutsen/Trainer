using CoreDomain.Base;
using System;

namespace CoreDomain.Events
{
    public sealed class ChallengePaused : DomainEvent
    {
        public ChallengePaused(TimeSpan duration,DateTime eventDateTime) : base(eventDateTime)
        {
            Duration = duration;
        }

        public TimeSpan Duration { get; }
    }
}
