using CoreDomain.Base;
using Domain.Identity;
using System;

namespace CoreDomain.Events
{
    public sealed class ChallengeCreated : DomainEvent
    {
        public ChallengeCreated(ChallengeId id,int repetitions, string description, DateTime eventDateTime) : base(eventDateTime)
        {
            Id = id;
            Repetitions = repetitions;
            Description = description;
        }

        public ChallengeId Id { get; }
        public int Repetitions { get; }
        public string Description { get; }
    }
}
