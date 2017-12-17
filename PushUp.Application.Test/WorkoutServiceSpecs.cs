using Moq;
using Nito.AsyncEx;
using NUnit.Framework;
using PushUp.Application.Commands;
using SpecsFor;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace PushUp.Application.Test
{
    class WorkoutServiceSpecs
    {
        static void InitSut(SpecsFor<WorkoutService> state, params IContext<WorkoutService>[] contexts)
        {
            foreach(var context in contexts)
            {
                context.Initialize(state);
            }
            state.SUT = new WorkoutService(state.GetMockFor<IEventStore>().Object, state.GetMockFor<IRepository<WorkoutViewModel>>().Object);
        }
        public class When_create_workout : SpecsFor<WorkoutService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this);
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new CreateWorkout(Guid.NewGuid(),Guid.NewGuid(), "workouttype", 10)));
            }
            [Test]
            public void Then_view_model_should_be_created()
            {
                GetMockFor<IRepository<WorkoutViewModel>>().Verify(r => r.AddAsync(It.IsAny<WorkoutViewModel>()), Times.Once);
            }
        }
    }
}
