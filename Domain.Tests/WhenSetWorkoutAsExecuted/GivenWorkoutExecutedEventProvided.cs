using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using Should;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using CoreDomain.Types;
using NUnit.Framework;

namespace Domain.Tests.WhenSetWorkoutAsExecuted
{
    public class GivenWorkoutExecutedEventProvided : SpecsFor<WorkoutState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new WorkoutState(new List<IEvent> {
                new WorkoutCreated(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(), It.IsAny<WorkoutType>(), It.IsAny<int>(),It.IsAny<DateTime>()),
                new WorkoutExecuted(It.IsAny<ChallengeId>(),It.IsAny<int>(),DateTime.Now)
            });
        }
        [Test]
        public void Then_state_should_be_executed()
        {
            SUT.Executed.ShouldBeTrue();
        }
        [Test]
        public void Then_time_for_execution_should_be_set()
        {
            SUT.PerformedOn.ShouldBeInRange(DateTime.Now.AddSeconds(-2), DateTime.Now);
        }
    }
}
