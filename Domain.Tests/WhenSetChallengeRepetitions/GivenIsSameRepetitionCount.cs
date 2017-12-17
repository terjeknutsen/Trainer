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
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.WhenSetChallengeRepetitions
{
    public class GivenIsSameRepetitionCount : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(), It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.SetDailyRepetitions(100, It.IsAny<DateTime>());
        }
        [Test]
        public void Then_repetitions_changed_event_should_not_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
