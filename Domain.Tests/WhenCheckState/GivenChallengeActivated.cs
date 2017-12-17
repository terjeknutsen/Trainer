using System;
using System.Collections.Generic;
using SpecsFor;
using Should;
using Moq;
using NUnit.Framework;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;

namespace Domain.Tests.WhenCheckState
{
    public class GivenChallengeActivated : SpecsFor<ChallengeState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new ChallengePaused(It.IsAny<TimeSpan>(), DateTime.Now.AddHours(-20)),
                new ChallengeActivated(DateTime.Now)
            });
        }

        [Test]
        public void Then_is_paused_property_should_be_set_to_false()
        {
            SUT.IsPaused.ShouldBeFalse();
        }
        [Test]
        public void Then_paused_duration_should_be_set_correctly()
        {
            SUT.PausedDuration.ShouldBeInRange(TimeSpan.FromMinutes((20*60)-1), TimeSpan.FromHours(20));
        }

    }
}
