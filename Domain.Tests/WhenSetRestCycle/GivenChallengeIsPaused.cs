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

namespace Domain.Tests.WhenSetRestCycle
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
            SUT.SetRestingCycle(new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, true }, { 4, true }, { 5, true }, { 6, true }, { 7, true } }, It.IsAny<DateTime>());
        }
        [Test]
        public void Then_resting_cycle_should_NOT_change()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
