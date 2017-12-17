using CoreDomain.Events;
using CoreDomain.Types;
using Domain.Core.Base;
using Domain.Core.States;
using Domain.Identity;
using DomainInterfaces;
using System;
using System.Collections.Generic;

namespace CoreDomain
{
    public sealed class Workout : AggregateRoot
    {
        private WorkoutState state;

        public Workout(IEnumerable<IEvent> events)
        {
            State = new WorkoutState(events);
            state = State as WorkoutState;
        }
        public void Create(WorkoutId id,ChallengeId challengeId,WorkoutType type, int repetitions)
        {
            if(State.Created)
            throw new InvalidOperationException($"{nameof(Workout)}-{id} already created");
            Apply(new WorkoutCreated(id,challengeId,type,repetitions,DateTime.Now));
        }

        public void Execute(DateTime executionDate)
        {
            if (state.Executed) return;

            Apply(new WorkoutExecuted(state.ChallengeId,state.Reps,executionDate));
        }
    }
}
