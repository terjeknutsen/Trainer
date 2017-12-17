using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using NUnit.Framework;
using Should;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;

namespace Domain.Tests.WhenChangeDescription
{
    public class GivenSameDescriptionAlreadyProvided : SpecsFor<Challenge>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Challenge(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new ChallengeDescriptionChanged("Description", It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.SetDescription("Description", It.IsAny<DateTime>());
        }
        [Test]
        public void Then_description_changed_event_should_NOT_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
