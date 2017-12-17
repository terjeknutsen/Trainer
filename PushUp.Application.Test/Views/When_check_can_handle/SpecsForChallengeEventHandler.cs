using CoreDomain.Events;
using Domain.Identity;
using Moq;
using NUnit.Framework;
using PushUp.Application.EventHandlers;
using Should;
using SpecsFor;
using System;
using System.Collections.Generic;

namespace PushUp.Application.Test.Views.When_check_can_handle
{
    class SpecsForChallengeEventHandler
    {
        public class When_check_can_handle_given_events_are_present  : SpecsFor<ChallengeEventHandler>
        {
            [Test]
            public void Then_can_handle_should_be_true_for_challenge_created()
            {
                SUT.CanHandle(new ChallengeCreated(It.IsAny<ChallengeId>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_challenge_activated()
            {
                SUT.CanHandle(new ChallengeActivated(It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_challenge_paused()
            {
                SUT.CanHandle(new ChallengePaused(It.IsAny<TimeSpan>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_challenge_duration_changed()
            {
                SUT.CanHandle(new ChallengeDurationChanged(It.IsAny<TimeSpan>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_daily_repetitions_changed()
            {
                SUT.CanHandle(new DailyRepetitionsChanged(It.IsAny<int>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_challenge_description_changed()
            {
                SUT.CanHandle(new ChallengeDescriptionChanged(It.IsAny<string>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_resting_cycle_changed()
            {
                SUT.CanHandle(new RestingCycleChanged(It.IsAny<IDictionary<int,bool>>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_should_be_true_for_workout_executed()
            {
                SUT.CanHandle(new WorkoutExecuted(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<DateTime>())).ShouldBeTrue();
            }

        }
    }
}
