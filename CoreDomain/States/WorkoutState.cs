using Domain.Core.Base;
using System.Collections.Generic;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using CoreDomain.Types;
using System;

namespace Domain.Core.States
{
    public sealed class WorkoutState : State
    {
        public WorkoutState(IEnumerable<IEvent> events) : base(events)
        {
        }

        public WorkoutId Id { get; private set; }
        public ChallengeId ChallengeId { get; set; } 
        public WorkoutType  Type{ get; private set; }
        public int Reps { get; private set; }
        public bool Executed { get; private set; }
        public DateTime PerformedOn { get; private set; }
        public DateTime TimeToPerform { get; set; }

        public void When(WorkoutCreated @event)
        {
            Created = true;
            Id = @event.Id;
            Type = @event.Type;
            Reps = @event.Reps;
            ChallengeId = @event.ChallengeId;
        }

        public void When(WorkoutExecuted @event)
        {
            Executed = true;
            PerformedOn = @event.OccurredOn;
        }
    }
}
