using Domain.Core.Base;
using System;
using System.Collections.Generic;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;

namespace Domain.Core.States
{
    public sealed class ChallengeState : State
    {
        public ChallengeState(IEnumerable<IEvent> events) : base(events)
        {
        }

        public ChallengeId Id { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartDate { get; set; }
        public int DailyRepetitions { get; set; }
        public string Description { get; set; }
        public IDictionary<int, bool> RestingCycle { get; set; } = new Dictionary<int, bool>
        {
            {1,true },
            {2,true },
            {3,true },
            {4,true },
            {5,true },
            {6,true },
            {7,true }
        };
        public bool IsPaused { get; set; }
        public DateTime PausedDateTime { get; set; }
        public TimeSpan PausedDuration { get; set; }

        public void When(ChallengeCreated @event)
        {
            Created = true;
            Id = @event.Id;
            StartDate = @event.OccurredOn;
            Duration = TimeSpan.FromDays(30);
            DailyRepetitions = 100;
            Description = string.Empty;
        }
        public void When(ChallengeDurationChanged @event)
        {
            Duration = @event.Duration;
        }
        public void When(DailyRepetitionsChanged @event)
        {
            DailyRepetitions = @event.Repetitions;
        }
        public void When(ChallengeDescriptionChanged @event)
        {
            Description = @event.Description;
        }
        public void When(RestingCycleChanged @event)
        {
            RestingCycle = @event.RestingCycle;
        }
        public void When(ChallengePaused @event)
        {
            IsPaused = true;
            PausedDateTime = @event.OccurredOn;
        }
        public void When(ChallengeActivated @event)
        {
            IsPaused = false;
            PausedDuration = @event.OccurredOn - PausedDateTime;
        }
    }
}
