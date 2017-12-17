using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using NUnit.Framework;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using Should;
using SpecsFor.ShouldExtensions;

namespace Domain.Tests.WhenSetChallengeRepetitions
{
    public class GivenIsDifferentRepetitionCountThanDefault : SpecsFor<Challenge>
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
            SUT.SetDailyRepetitions(75, It.IsAny<DateTime>());
        }
        [Test]
        public void Then_daily_repetitions_changed_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(DailyRepetitionsChanged));
        }

    }
}
