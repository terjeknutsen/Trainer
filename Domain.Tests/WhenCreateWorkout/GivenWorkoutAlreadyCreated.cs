using System;
using System.Collections.Generic;
using SpecsFor;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using NUnit.Framework;
using Moq;
using Domain.Identity;
using CoreDomain.Types;

namespace Domain.Tests.WhenCreateWorkout
{
    public class GivenWorkoutAlreadyCreated : SpecsFor<Workout>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Workout(new List<IEvent> { new WorkoutCreated(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(), It.IsAny<WorkoutType>(), It.IsAny<int>(),It.IsAny<DateTime>()) });
        }
        [Test]
        public void Then_an_invalid_operation_exception_should_be_thrown()
        {
            Assert.Throws<InvalidOperationException>(() => SUT.Create(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(),It.IsAny<WorkoutType>(),It.IsAny<int>()));
        }
    }
}
