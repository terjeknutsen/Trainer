using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using NUnit.Framework;
using Should;

namespace Domain.Tests.WhenActivateChallenge
{
    public class GivenChallengeIsActive : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
            });
        }
        protected override void When()
        {
            SUT.Activate(It.IsAny<DateTime>());
        }
        [Test]
        public void Then_challenge_activated_event_should_not_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
