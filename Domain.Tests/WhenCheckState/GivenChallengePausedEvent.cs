using CoreDomain.Events;
using Domain.Core.States;
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

namespace Domain.Tests.WhenCheckState
{
    public class GivenChallengePausedEvent : SpecsFor<ChallengeState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new ChallengePaused(It.IsAny<TimeSpan>(), DateTime.Now)
            });
        }
        [Test]
        public void Then_is_paused_property_should_be_true()
        {
            SUT.IsPaused.ShouldBeTrue();
        }
        [Test]
        public void Then_paused_date_should_be_correctly_set()
        {
            SUT.PausedDateTime.ShouldBeInRange(DateTime.Now.AddSeconds(-3), DateTime.Now);
        }
    }
}
