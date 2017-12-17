using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class GivenChallengeDescriptionChangedEvent : SpecsFor<ChallengeState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new ChallengeDescriptionChanged("Description", It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_description_should_be_set()
        {
            SUT.Description.ShouldEqual("Description");
        }
    }
}
