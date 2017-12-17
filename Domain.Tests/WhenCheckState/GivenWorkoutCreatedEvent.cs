using System;
using System.Collections.Generic;
using SpecsFor;
using Should;
using Domain.Core.States;
using DomainInterfaces;
using CoreDomain.Events;
using NUnit.Framework;
using Domain.Identity;
using CoreDomain.Types;
using SpecsFor.ShouldExtensions;
using Moq;

namespace Domain.Tests.WhenCheckState
{
    public class GivenWorkoutCreatedEvent : SpecsFor<WorkoutState>
    {
        readonly WorkoutId workoutId = new WorkoutId(Guid.NewGuid());
        readonly ChallengeId challengeId = new ChallengeId(Guid.NewGuid());
        readonly WorkoutType workoutType = new WorkoutType("PushUp");
        protected override void InitializeClassUnderTest()
        {
            SUT = new WorkoutState(new List<IEvent> { new WorkoutCreated(workoutId,challengeId, workoutType, 20,It.IsAny<DateTime>())});
        }
        [Test]
        public void Then_identity_should_be_set()
        {
            SUT.Id.ShouldLookLike(workoutId);
        }
        [Test]
        public void Then_type_should_be_set()
        {
            SUT.Type.ShouldLookLike(workoutType);
        }
        [Test]
        public void Then_repetitions_should_be_set()
        {
            SUT.Reps.ShouldEqual(20);
        }
        [Test]
        public void Then_challenge_id_should_be_set()
        {
            SUT.ChallengeId.ShouldLookLike(challengeId);
        }
    }
}
