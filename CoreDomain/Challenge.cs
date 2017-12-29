using CoreDomain.Enteties;
using CoreDomain.Events;
using Domain.Core.Base;
using Domain.Core.States;
using Domain.Identity;
using DomainInterfaces;
using System;
using System.Collections.Generic;

namespace CoreDomain
{
    public sealed class Challenge : AggregateRoot
    {
        readonly ChallengeState state;
        public Challenge(IEnumerable<IEvent> events)
        {
            State = new ChallengeState(events);
            state = State as ChallengeState;
        }

        public void Create(ChallengeId id,int repetitions,string description)
        {
            if(State.Created)
            throw new InvalidOperationException($"{nameof(Challenge)} already created");
            Apply(new ChallengeCreated(id,repetitions,description,DateTime.Now));
        }
        public void SetDuration(TimeSpan timeSpan,DateTime eventDateTime)
        {
            if (state.Duration != timeSpan)
            Apply(new ChallengeDurationChanged(timeSpan, eventDateTime));
        }
        public void SetDailyRepetitions(int repetitions, DateTime dateTime)
        {
            if (state.DailyRepetitions != repetitions)
            Apply(new DailyRepetitionsChanged(repetitions, dateTime));
        }
        public void SetDescription(string description, DateTime dateTime)
        {
            if (state.Description != description)
            Apply(new ChallengeDescriptionChanged(description, dateTime));
        }
        public void SetRestingCycle(IDictionary<int,bool> restingCycle, DateTime dateTime)
        {
            if (state.IsPaused)
                return;

            foreach(var keyValue in state.RestingCycle)
            {
                if (restingCycle.ContainsKey(keyValue.Key))
                {
                    if(restingCycle[keyValue.Key] != keyValue.Value)
                    {
                        Apply(new RestingCycleChanged(restingCycle, dateTime));
                        break;
                    }
                }
            }
        }
        public void Pause(DateTime dateTime)
        {
            if (!state.IsPaused)
            Apply(new ChallengePaused(state.Duration,dateTime));
        }
        public void Activate(DateTime dateTime)
        {
            if(state.IsPaused)
            Apply(new ChallengeActivated(dateTime));
        }
        public void SetWorkoutSchedule(WorkoutSchedule schedule,DateTime dateTime)
        {
            Apply(new WorkoutScheduleChanged(schedule.Schedule, dateTime));
        }
    }
}
