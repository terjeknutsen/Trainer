using CoreDomain.Base;
using Domain.Identity;
using System;

namespace CoreDomain.Events
{
    public sealed class WorkoutExecuted : DomainEvent
    {
        public WorkoutExecuted(ChallengeId challengeId, int repetitions,DateTime eventDateTime):base(eventDateTime)
        {
            ChallengeId = challengeId;
            Repetitions = repetitions;
        }

        public ChallengeId ChallengeId { get; }
        public int Repetitions { get; }
    }
}
