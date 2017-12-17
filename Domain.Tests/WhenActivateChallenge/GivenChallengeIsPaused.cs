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


namespace Domain.Tests.WhenActivateChallenge
{
    public class GivenChallengeIsPaused : SpecsFor<Challenge>
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
            SUT.Activate(It.IsAny<DateTime>());
        }
        [Test]
        public void Then_challenge_activated_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(ChallengeActivated));
        }
    }
}
