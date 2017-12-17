using CoreDomain.Events;
using Domain.Identity;
using Moq;
using Nito.AsyncEx;
using NUnit.Framework;
using PushUp.Application.EventHandlers;
using Should;
using SpecsFor;
using SpecsFor.ShouldExtensions;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace PushUp.Application.Test.Views.When_update_view
{
    class SpecsForChallengeEventHandler
    {
        public class When_create_challenge : SpecsFor<ChallengeEventHandler>
        {
            protected override void When()
            {
               AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new ChallengeCreated(new ChallengeId(Guid.NewGuid()), 22, "Test", DateTime.Now)));
            }
            [Test]
            public void Then_challenge_view_model_should_be_created()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.AddAsync(It.IsAny<ChallengeViewModel>()), Times.Once);
            }
        }
        public class When_change_duration : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new ChallengeDurationChanged(TimeSpan.FromDays(20), DateTime.Now)));
            }
            [Test]
            public void Then_view_model_should_be_fetched_from_repository()
            {
                GetMockFor<IRepository<ChallengeViewModel>>().Verify(r => r.GetAsync(It.IsAny<Guid>()), Times.Once);
            }
            [Test]
            public void Then_duration_should_be_changed_and_saved()
            {
                Callback.Duration.ShouldLookLike(TimeSpan.FromDays(20));
            }
        }
        public class When_daily_repetitions_changed : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new DailyRepetitionsChanged(22, DateTime.Now)));
            }
            [Test]
            public void Then_repetitions_should_be_changed_and_saved()
            {
                Callback.DailyRepetitions.ShouldEqual(22);
            }
        }

        public class When_description_changed : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new ChallengeDescriptionChanged("description", DateTime.Now)));
            }
            [Test]
            public void Then_description_should_be_changed_and_saved()
            {
                Callback.Desciption.ShouldEqual("description");
            }
        }

        public class When_restingCycle_changed : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new RestingCycleChanged(new Dictionary<int, bool>
                {
                    {1,false },
                    {2,true },
                    {3,false },
                    {4, true },
                    {5,false },
                    {6,true },
                    {7,false }
                }, DateTime.Now)));
            }
            [Test]
            public void Then_resting_cycle_should_be_correct()
            {
                Callback.ActiveMonday.ShouldBeFalse();
                Callback.ActiveTuesday.ShouldBeTrue();
                Callback.ActiveWednesday.ShouldBeFalse();
                Callback.ActiveThursday.ShouldBeTrue();
                Callback.ActiveFriday.ShouldBeFalse();
                Callback.ActiveSaturday.ShouldBeTrue();
                Callback.ActiveSunday.ShouldBeFalse();
            }
        }

        public class When_challenge_paused : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new ChallengePaused(TimeSpan.FromDays(2),DateTime.Now)));
            }
            [Test]
            public void Then_Then_challenge_should_be_paused()
            {
                Callback.IsPaused.ShouldBeTrue();
            }
            [Test]
            public void Then_paused_date_should_be_set()
            {
                Callback.PausedDateTime.ShouldBeInRange(DateTime.Now.AddSeconds(-1), DateTime.Now);
            }
            [Test]
            public void Then_paused_duration_should_be_set()
            {
                Callback.PausedDuration.ShouldLookLike(TimeSpan.FromDays(2));
            }

        }

        public class When_challenge_activated : SpecsFor<ChallengeEventHandler>
        {
            protected override void Given()
            {
                Given<ChallengeViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new ChallengeId(Guid.NewGuid()), new ChallengeActivated(DateTime.Now)));
            }
            [Test]
            public void Then_Then_challenge_should_be_active()
            {
                Callback.IsPaused.ShouldBeFalse();
            }
        }

        public class When_workout_executed : SpecsFor<ChallengeEventHandler>
        {
            private ChallengeId challengeId = new ChallengeId(Guid.NewGuid());
            protected override void Given()
            {
                Given(new ChallengeViewModelExistWithId(challengeId));
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new WorkoutId(Guid.NewGuid()), new WorkoutExecuted(challengeId,20,DateTime.Now)));
            }
            [Test]
            public void Then_total_repetitions_should_be_increased_with_workout_repetitions()
            {
                Callback.TotalRepetitions.ShouldEqual(20);
            }
        }
        static ChallengeViewModel Callback { get; set; }
        class CallbackSetup : IContext<ChallengeEventHandler>
        {
            public void Initialize(ISpecs<ChallengeEventHandler> state)
            {
                state.GetMockFor<IRepository<ChallengeViewModel>>().Setup(r => r.UpdateAsync(It.IsAny<ChallengeViewModel>()))
                    .Callback((ChallengeViewModel c)=> Callback = c)
                    .Returns(Task.CompletedTask);
            }
        }
        class ChallengeViewModelExist : IContext<ChallengeEventHandler>
        {
            public void Initialize(ISpecs<ChallengeEventHandler> state)
            {
                state.GetMockFor<IRepository<ChallengeViewModel>>().Setup(r => r.GetAsync(It.IsAny<Guid>()))
                    .Returns(Task.FromResult(new ChallengeViewModel()));
            }
        }

        class ChallengeViewModelExistWithId : IContext<ChallengeEventHandler>
        {
            private readonly ChallengeId id;

            public ChallengeViewModelExistWithId(ChallengeId id)
            {
                this.id = id;
            }
            public void Initialize(ISpecs<ChallengeEventHandler> state)
            {
                state.GetMockFor<IRepository<ChallengeViewModel>>().Setup(r => r.GetAsync(id.Guid))
                    .Returns(Task.FromResult(new ChallengeViewModel()));
            }
        }
    }
}
