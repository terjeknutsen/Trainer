using StorageInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;
using ViewModels;
using CoreDomain.Events;
using System;

namespace PushUp.Application.EventHandlers 
{
    public sealed class ChallengeEventHandler : EventHandler<ChallengeViewModel>
    {
        public ChallengeEventHandler(IRepository<ChallengeViewModel> repository):base(repository)
        {
            Actions = new Dictionary<string, Func<IAggregateIdentity, IEvent,Task>>
        {
            {nameof(ChallengeCreated), CreateView },
            {nameof(ChallengeDurationChanged), ChangeDuration },
            {nameof(DailyRepetitionsChanged), ChangeDailyRepetitions },
            {nameof(ChallengeDescriptionChanged), ChangeDescription },
            {nameof(RestingCycleChanged), ChangeRestingCycle },
            {nameof(ChallengePaused), PauseChallenge },
            {nameof(ChallengeActivated), ActivateChallenge },
            {nameof(WorkoutExecuted), AddRepetitions },
            {nameof(WorkoutScheduleChanged), SetWorkoutSchedule }
        };
        }

        

        private async Task CreateView(IAggregateIdentity id, IEvent @event)
        {
            if (@event is ChallengeCreated challengeCreated)
            {
                await Repository.AddAsync(new ChallengeViewModel
                {
                    Id = challengeCreated.Id.Guid,
                    StartDate = challengeCreated.OccurredOn,
                    Desciption = challengeCreated.Description,
                    DailyRepetitions = challengeCreated.Repetitions
                });
            }
        }
        private async Task ChangeDuration(IAggregateIdentity id, IEvent @event)
        {
            if (@event is ChallengeDurationChanged durationChanged)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Duration = durationChanged.Duration;
                await Repository.UpdateAsync(viewModel);
            }

        }
        private async Task ChangeDailyRepetitions(IAggregateIdentity id, IEvent @event)
        {
            if (@event is DailyRepetitionsChanged dailyRepetitionsChanged)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.DailyRepetitions = dailyRepetitionsChanged.Repetitions;
                await Repository.UpdateAsync(viewModel);
            }
        }
        private async Task ChangeDescription(IAggregateIdentity id, IEvent @event)
        {
            if (@event is ChallengeDescriptionChanged descriptionChanged)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Desciption = descriptionChanged.Description;
                await Repository.UpdateAsync(viewModel);
            }
        }
        private async Task ChangeRestingCycle(IAggregateIdentity id, IEvent @event)
        {
            if(@event is RestingCycleChanged changeEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.ActiveMonday = changeEvent.RestingCycle[1];
                viewModel.ActiveTuesday = changeEvent.RestingCycle[2];
                viewModel.ActiveWednesday = changeEvent.RestingCycle[3];
                viewModel.ActiveThursday = changeEvent.RestingCycle[4];
                viewModel.ActiveFriday = changeEvent.RestingCycle[5];
                viewModel.ActiveSaturday = changeEvent.RestingCycle[6];
                viewModel.ActiveSunday = changeEvent.RestingCycle[7];
                await Repository.UpdateAsync(viewModel);
            }
        }
        private async Task PauseChallenge(IAggregateIdentity id, IEvent @event)
        {
            if(@event is ChallengePaused changeEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.IsPaused = true;
                viewModel.PausedDateTime = changeEvent.OccurredOn;
                viewModel.PausedDuration = changeEvent.Duration;
                await Repository.UpdateAsync(viewModel);
            }
        }
        private async Task ActivateChallenge(IAggregateIdentity id, IEvent @event)
        {
            if(@event is ChallengeActivated changeEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.IsPaused = false;
                await Repository.UpdateAsync(viewModel);
            }
        }

        async Task AddRepetitions(IAggregateIdentity id, IEvent @event)
        {
            if(@event is WorkoutExecuted changeEvent)
            {
                var viewModel = await Repository.GetAsync(changeEvent.ChallengeId.Guid);
                viewModel.TotalRepetitions += changeEvent.Repetitions;
                await Repository.UpdateAsync(viewModel);
            }
        }
        async Task SetWorkoutSchedule(IAggregateIdentity id, IEvent @event)
        {
            if(@event is WorkoutScheduleChanged changeEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Schedule = changeEvent.Schedule;
                await Repository.UpdateAsync(viewModel);
            }
        }
    }
}
