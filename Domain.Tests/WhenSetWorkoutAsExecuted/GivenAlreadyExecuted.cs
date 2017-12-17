using CoreDomain;
using CoreDomain.Events;
using CoreDomain.Types;
using Domain.Identity;
using DomainInterfaces;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.WhenSetWorkoutAsExecuted
{
    public class GivenAlreadyExecuted : SpecsFor<Workout>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Workout(new List<IEvent>
            {
                new WorkoutCreated(It.IsAny<WorkoutId>(),It.IsAny<ChallengeId>(),It.IsAny<WorkoutType>(),It.IsAny<int>(),DateTime.Now),
                new WorkoutExecuted(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<DateTime>())
            });
        }

        protected override void When()
        {
            SUT.Execute(DateTime.Now);
        }
        [Test]
        public void Then_no_changes_should_be_made()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
