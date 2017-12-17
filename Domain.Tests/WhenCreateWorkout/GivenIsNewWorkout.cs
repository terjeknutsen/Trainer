using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using CoreDomain;
using DomainInterfaces;
using Domain.Identity;
using NUnit.Framework;
using Should;
using CoreDomain.Events;
using CoreDomain.Types;

namespace Domain.Tests.WhenCreateWorkout
{
    public class GivenIsNewWorkout : SpecsFor<Workout>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Workout(new List<IEvent>());
        }
        protected override void When()
        {
            SUT.Create(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(),It.IsAny<WorkoutType>(),It.IsAny<int>());
        }
        [Test]
        public void Then_workout_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(WorkoutCreated));
        }
    }
}
