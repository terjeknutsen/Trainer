using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Should;
using Moq;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using NUnit.Framework;
using SpecsFor.ShouldExtensions;

namespace Domain.Tests.WhenCheckState
{
    public class GivenChallengeCreatedEvent : SpecsFor<ChallengeState>
    {
        readonly ChallengeId challengeId = new ChallengeId(Guid.NewGuid()); 
        protected override void InitializeClassUnderTest()
        {
            SUT = new ChallengeState(new List<IEvent> { new ChallengeCreated(challengeId,It.IsAny<int>(),It.IsAny<string>(),DateTime.Now) });
        }
        [Test]
        public void Then_id_should_be_set()
        {
            SUT.Id.ShouldLookLike(challengeId);
        }
        [Test]
        public void Then_start_date_should_be_set()
        {
            SUT.StartDate.ShouldBeInRange(DateTime.Now.AddSeconds(-3), DateTime.Now);
        }
        [Test]
        public void Then_default_duration_should_be_set()
        {
            SUT.Duration.ShouldLookLike(TimeSpan.FromDays(30));
        }
        [Test]
        public void Then_default_daily_repetitions_should_be_set()
        {
            SUT.DailyRepetitions.ShouldEqual(100);
        }
        [Test]
        public void Then_description_should_be_empty()
        {
            SUT.Description.ShouldEqual(string.Empty);
        }
    }
}
