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

namespace Domain.Tests.WhenSetChallengeDuration
{
    public class GivenIsDifferentDurationThanDefault : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent> { new ChallengeCreated(new ChallengeId(Guid.NewGuid()), It.IsAny<int>(), It.IsAny<string>(), DateTime.Now) });
        }
        protected override void When()
        {
            SUT.SetDuration(TimeSpan.FromDays(14),DateTime.Now);
        }
        [Test]
        public void Then_duration_changed_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(ChallengeDurationChanged));
        }
    }
}
