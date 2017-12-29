using CoreDomain;
using CoreDomain.Events;
using Domain.Identity;
using DomainInterfaces;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Tests.WhenSetWorkoutSchedule
{
    public class GivenScheduleNotSet : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent> { new ChallengeCreated(It.IsAny<ChallengeId>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())});
        }

        protected override void When()
        {
            SUT.SetWorkoutSchedule(new CoreDomain.Enteties.WorkoutSchedule
            {
                Schedule = new List<KeyValuePair<TimeSpan, int>>
                {
                    new KeyValuePair<TimeSpan, int>(TimeSpan.FromHours(2), 20)
                }
            },It.IsAny<DateTime>());
        }
        [Test]
        public void Then_workout_schedule_should_be_set()
        {
            SUT.Changes.Last().ShouldBeType(typeof(WorkoutScheduleChanged));
        }
    }
}
