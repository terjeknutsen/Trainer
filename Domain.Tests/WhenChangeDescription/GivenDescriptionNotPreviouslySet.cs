using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Should;
using Moq;
using NUnit.Framework;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;

namespace Domain.Tests.WhenChangeDescription
{
    public class GivenDescriptionNotPreviouslySet  : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.SetDescription(It.IsAny<string>(), It.IsAny<DateTime>());
        }
        [Test]
        public void Then_challenge_description_changed_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(ChallengeDescriptionChanged));
        }
    }
}
