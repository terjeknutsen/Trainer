using CoreDomain;
using DomainInterfaces;
using NUnit.Framework;
using SpecsFor;
using System.Collections.Generic;
using System.Linq;
using Should;
using CoreDomain.Events;
using Moq;
using Domain.Identity;

namespace Domain.Tests.WhenCreateChallenge
{
    public class GivenIsNewChallenge : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>());
        }
        protected override void When()
        {
            SUT.Create(It.IsAny<ChallengeId>(), It.IsAny<int>(), It.IsAny<string>());
        }
        [Test]
        public void Then_challenge_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(ChallengeCreated));
        }
    }
}
