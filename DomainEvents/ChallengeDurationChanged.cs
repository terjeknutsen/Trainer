using System;

namespace DomainEvents
{
    public sealed class ChallengeDurationChanged : DomainEvent
    {
        public ChallengeDurationChanged(ChallengeId id,TimeSpan duration,DateTime eventDateTime)
        :base(eventDateTime){
            Id = id;
            Duration = duration;
        }

        public ChallengeId Id { get; }
        public TimeSpan Duration { get; }
    }
}
