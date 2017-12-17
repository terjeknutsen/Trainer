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
using SpecsFor.ShouldExtensions;

namespace Domain.Tests.WhenCheckState
{
    public class GivenRestingCycleChangedEvent : SpecsFor<ChallengeState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new RestingCycleChanged(new Dictionary<int,bool>{ {1,true },{ 2, true },{ 3, false },{ 4, true },{5,true },{6,true },{ 7, true } },It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_resting_cycle_should_be_set()
        {
            SUT.RestingCycle.ShouldLookLike(new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, false }, { 4, true }, { 5, true }, { 6, true }, { 7, true } });
        }
    }
}
