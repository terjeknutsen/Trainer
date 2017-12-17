using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using NUnit.Framework;
using Moq;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using Domain.Identity;
using Should;
using SpecsFor.ShouldExtensions;

namespace Domain.Tests.WhenCheckState
{
    public class GivenChallengeDurationChangedEvent : SpecsFor<ChallengeState>
    {
        protected override void InitializeClassUnderTest()
        {

            SUT = new ChallengeState(new List<IEvent>
            {
                new ChallengeCreated(new ChallengeId(Guid.NewGuid()),It.IsAny<int>(),It.IsAny<string>(),DateTime.Now.AddSeconds(-10)),
                new ChallengeDurationChanged(TimeSpan.FromDays(30),DateTime.Now)
            });
        }
       [Test]
       public void Then_challenge_duration_should_be_set()
        {
            SUT.Duration.ShouldLookLike(TimeSpan.FromDays(30));
        }
    }
}
