using CoreDomain.Base;
using CoreDomain.Types;
using Domain.Identity;
using System;

namespace CoreDomain.Events
{
    public sealed class WorkoutCreated : DomainEvent
    {
        public WorkoutCreated(WorkoutId id,ChallengeId challengeId,WorkoutType type, int reps,DateTime eventDateTime):base(eventDateTime)
        {
            Id = id;
            ChallengeId = challengeId;
            Type = type;
            Reps = reps;
        }

        public WorkoutId Id { get; }
        public ChallengeId ChallengeId { get; }
        public WorkoutType Type { get; }
        public int Reps { get; }
    }
}
