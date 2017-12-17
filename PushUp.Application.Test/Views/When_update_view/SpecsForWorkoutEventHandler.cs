using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using Should;
using PushUp.Application.EventHandlers;
using Nito.AsyncEx;
using Domain.Identity;
using CoreDomain.Events;
using CoreDomain.Types;
using NUnit.Framework;
using StorageInterfaces;
using ViewModels;

namespace PushUp.Application.Test.Views.When_update_view
{
    class SpecsForWorkoutEventHandler
    {
        public class When_update_from_workout_created_event : SpecsFor<WorkoutEventHandler>
        {
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(It.IsAny<WorkoutId>(), new WorkoutCreated(new WorkoutId(Guid.NewGuid()),new ChallengeId(Guid.NewGuid()), new WorkoutType("workout"), 20, It.IsAny<DateTime>())));
            }
            [Test]
            public void Then_workout_should_be_added_to_repository()
            {
                GetMockFor<IRepository<WorkoutViewModel>>().Verify(r => r.AddAsync(It.IsAny<WorkoutViewModel>()), Times.Once);
            }
        }

        public class When_update_from_workout_executed_event : SpecsFor<WorkoutEventHandler>
        {
            protected override void Given()
            {
                Given<WorkoutViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new WorkoutId(Guid.NewGuid()), new WorkoutExecuted(It.IsAny<ChallengeId>(),It.IsAny<int>(),DateTime.Now)));
            }
            [Test]
            public void Then_workout_should_be_fetched_from_repository()
            {
                GetMockFor<IRepository<WorkoutViewModel>>().Verify(r => r.GetAsync(It.IsAny<Guid>()), Times.Once);
            }
            [Test]
            public void Then_workout_should_be_set_as_executed()
            {
                Callback.Executed.ShouldBeTrue();
            }
            [Test]
            public void Then_workout_performed_on_should_be_set()
            {
                Callback.PerformedOn.ShouldBeInRange(DateTime.Now.AddSeconds(-1), DateTime.Now);
            }
        }
        static WorkoutViewModel Callback { get; set; }
        class CallbackSetup : IContext<WorkoutEventHandler>
        {
            public void Initialize(ISpecs<WorkoutEventHandler> state)
            {
                state.GetMockFor<IRepository<WorkoutViewModel>>()
                    .Setup(r => r.UpdateAsync(It.IsAny<WorkoutViewModel>()))
                .Callback((WorkoutViewModel w)=> Callback = w)
                .Returns(Task.CompletedTask);
            }
        }
        class WorkoutViewModelExist : IContext<WorkoutEventHandler>
        {
            public void Initialize(ISpecs<WorkoutEventHandler> state)
            {
                state.GetMockFor<IRepository<WorkoutViewModel>>().Setup(r => r.GetAsync(It.IsAny<Guid>()))
                    .Returns(Task.FromResult(new WorkoutViewModel()));
            }
        }
    }
}
