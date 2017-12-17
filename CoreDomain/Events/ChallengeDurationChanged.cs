using CoreDomain.Base;
using System;

namespace CoreDomain.Events
{
    public sealed class ChallengeDurationChanged : DomainEvent
    {
        public ChallengeDurationChanged(TimeSpan duration,DateTime eventDateTime)
        :base(eventDateTime){
            Duration = duration;
        }

        public TimeSpan Duration { get; }
    }
}
