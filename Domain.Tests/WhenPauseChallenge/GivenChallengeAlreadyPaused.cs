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

namespace Domain.Tests.WhenPauseChallenge
{
    public class GivenChallengeAlreadyPaused : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new ChallengePaused(It.IsAny<TimeSpan>(),It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.Pause(It.IsAny<DateTime>());
        }
        [Test]
        public void Then_challenge_paused_should_not_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
