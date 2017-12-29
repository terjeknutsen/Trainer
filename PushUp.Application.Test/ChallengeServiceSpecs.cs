using CoreDomain.Enteties;
using CoreDomain.Events;
using Domain.Identity;
using DomainInterfaces;
using Moq;
using Nito.AsyncEx;
using NUnit.Framework;
using PushUp.Application.Commands;
using SpecsFor;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace PushUp.Application.Test
{
    class ChallengeServiceSpecs
    {
        static void InitSut(SpecsFor<ChallengeService> state, params IContext<ChallengeService>[] contexts)
        {
            foreach(var context in contexts)
            {
                context.Initialize(state);
            }
            state.SUT = new ChallengeService(state.GetMockFor<IEventStore>().Object, state.GetMockFor<IRepository<ChallengeViewModel>>().Object);
        }
        public class When_create_challenge_command : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this);
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new CreateChallenge(Guid.NewGuid(),100,"description")));
            }
            [Test]
            public void Then_challenge_should_be_added_to_repository()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.AddAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }
        public class When_set_challenge_description : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetChallengeDescription(Guid.NewGuid(), "description")));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        public class When_set_challeng_duration : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetChallengeDuration(Guid.NewGuid(), TimeSpan.FromDays(29))));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        public class When_set_daily_repetitions : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetDailyRepetitions(Guid.NewGuid(), 90)));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        public class When_set_resting_cycle : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetRestingCycle(Guid.NewGuid(), true, false, true, true, true, true, true)));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        public class When_pause_challenge : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new PauseChallenge(Guid.NewGuid())));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(c => c.UpdateAsync(It.IsAny<ChallengeViewModel>()),Times.Once);
            }
        }
        public class When_activate_challenge : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this,new ChallengeIsPaused(), new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new ActivateChallenge(Guid.NewGuid())));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(c => c.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        public class When_change_workout_schedule : SpecsFor<ChallengeService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ChallengeViewModelExist());
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetWorkoutSchedule(Guid.NewGuid(), 
                    new List<KeyValuePair<TimeSpan, int>>
                    { 
                        new KeyValuePair<TimeSpan, int>(TimeSpan.FromHours(2),20)
                    }
                )));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(c => c.UpdateAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }

        class ChallengeViewModelExist : IContext<ChallengeService>
        {
            public void Initialize(ISpecs<ChallengeService> state)
            {
                state.GetMockFor<IRepository<ChallengeViewModel>>().Setup(c => c.GetAsync(It.IsAny<Guid>()))
                    .Returns(Task.FromResult(new ChallengeViewModel()));
            }
        }
        class ChallengeIsPaused : IContext<ChallengeService>
        {
            public void Initialize(ISpecs<ChallengeService> state)
            {
                state.GetMockFor<IEventStore>().Setup(e => e.Get(It.IsAny<IAggregateIdentity>()))
                    .Returns(new List<IEvent>
                    {
                        new ChallengeCreated(It.IsAny<ChallengeId>(),It.IsAny<int>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                        new ChallengePaused(It.IsAny<TimeSpan>(),It.IsAny<DateTime>())
                    });
            }
        }
    }
}
