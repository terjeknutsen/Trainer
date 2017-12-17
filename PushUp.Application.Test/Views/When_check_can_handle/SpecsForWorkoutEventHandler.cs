using CoreDomain.Events;
using CoreDomain.Types;
using Domain.Identity;
using Moq;
using NUnit.Framework;
using PushUp.Application.EventHandlers;
using Should;
using SpecsFor;
using System;

namespace PushUp.Application.Test.Views.When_check_can_handle
{
    class SpecsForWorkoutEventHandler
    {
        public class When_check_can_handle_given_events_setup : SpecsFor<WorkoutEventHandler>
        {
            [Test]
            public void Then_can_handle_workout_created_should_be_true()
            {
                SUT.CanHandle(new WorkoutCreated(new WorkoutId(Guid.NewGuid()),new ChallengeId(Guid.NewGuid()), new WorkoutType("workout"), 20, DateTime.Now)).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_workout_executed_should_be_true()
            {
                SUT.CanHandle(new WorkoutExecuted(It.IsAny<ChallengeId>(),It.IsAny<int>(),DateTime.Now)).ShouldBeTrue();
            }
        }
    }
}
