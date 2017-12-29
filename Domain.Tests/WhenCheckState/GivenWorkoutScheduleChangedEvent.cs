using System;
using System.Collections.Generic;
using System.Linq;
using SpecsFor;
using Moq;
using NUnit.Framework;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using CoreDomain.Types;
using SpecsFor.ShouldExtensions;

namespace Domain.Tests.WhenCheckState
{
    public class GivenWorkoutScheduleChangedEvent : SpecsFor<ChallengeState>
    {
        readonly WorkoutId workoutId = new WorkoutId(Guid.NewGuid());
        readonly ChallengeId challengeId = new ChallengeId(Guid.NewGuid());
        readonly WorkoutType workoutType = new WorkoutType("PushUp");
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(challengeId,20,It.IsAny<string>(),It.IsAny<DateTime>()),
                new WorkoutScheduleChanged(new List<KeyValuePair<TimeSpan, int>>{new KeyValuePair<TimeSpan, int>(TimeSpan.FromHours(2),20)}, It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_workout_schedule_should_be_set()
        {
            SUT.Schedule.First().ShouldLookLike(new KeyValuePair<TimeSpan, int>(TimeSpan.FromHours(2), 20));
        }

    }
}
