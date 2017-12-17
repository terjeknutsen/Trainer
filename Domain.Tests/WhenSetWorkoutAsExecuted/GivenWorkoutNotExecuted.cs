using System.Collections.Generic;
using System.Linq;
using SpecsFor;
using Should;
using Moq;
using CoreDomain;
using DomainInterfaces;
using NUnit.Framework;
using CoreDomain.Events;
using Domain.Identity;
using CoreDomain.Types;
using System;

namespace Domain.Tests.WhenSetWorkoutAsExecuted
{
    public class GivenWorkoutNotExecuted : SpecsFor<Workout>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Workout(new List<IEvent> { new WorkoutCreated(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(),It.IsAny<WorkoutType>(),It.IsAny<int>(),It.IsAny<DateTime>() )});
        }
        protected override void When()
        {
            SUT.Execute(It.IsAny<DateTime>());
        }
        [Test]
        public void Then_workout_should_be_set_as_executed()
        {
            SUT.Changes.Last().ShouldBeType(typeof(WorkoutExecuted));
        }
    }
}
