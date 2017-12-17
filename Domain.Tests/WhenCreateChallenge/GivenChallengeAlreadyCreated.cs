using System;
using SpecsFor;
using NUnit.Framework;
using Moq;
using Domain.Identity;
using CoreDomain;
using DomainInterfaces;
using System.Collections.Generic;
using CoreDomain.Events;

namespace Domain.Tests.WhenCreateChallenge
{
    public class GivenChallengeAlreadyCreated : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent> { new ChallengeCreated(new ChallengeId(Guid.NewGuid()),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()) });
        }
        [Test]
        public void Then_an_invalid_operation_exception_should_be_thrown()
        {
            Assert.Throws<InvalidOperationException>(() => SUT.Create(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>()));
        }
    }
}
