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

namespace Domain.Tests.WhenSetRestCycle
{
    public class GivenSameRestingCycleAlreadySet : SpecsFor<Challenge>
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
            SUT.SetRestingCycle(new Dictionary<int, bool> { {1,true }, { 2, true }, { 3, true }, { 4, true }, { 5, true }, { 6, true }, { 7, true } }, It.IsAny<DateTime>());
        }
        [Test]
        public void Then_resting_cycle_changed_event_should_NOT_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
