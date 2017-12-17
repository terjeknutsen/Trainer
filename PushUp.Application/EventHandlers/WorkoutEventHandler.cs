using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;
using ViewModels;
using CoreDomain.Events;

namespace PushUp.Application.EventHandlers
{
    public sealed class WorkoutEventHandler : EventHandler<WorkoutViewModel>
    {
        public WorkoutEventHandler(IRepository<WorkoutViewModel> repository) : base(repository)
        {
            Actions = new Dictionary<string, Func<IAggregateIdentity, IEvent, Task>>
            {
                {nameof(WorkoutCreated), CreateView },
                {nameof(WorkoutExecuted),SetWorkoutExecuted }
            };
        }
        async Task CreateView(IAggregateIdentity id, IEvent @event)
        {
            if(@event is WorkoutCreated workoutCreated)
            {
                var viewModel = new WorkoutViewModel
                {
                    Id = workoutCreated.Id.Guid,
                    Reps = workoutCreated.Reps,
                    WorkoutType = workoutCreated.Type.ToString()
                };
                await Repository.AddAsync(viewModel);
            }

        }
        async Task SetWorkoutExecuted(IAggregateIdentity id, IEvent @event)
        {
            if(@event is WorkoutExecuted workoutExecuted)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Executed = true;
                viewModel.PerformedOn = workoutExecuted.OccurredOn;
                await Repository.UpdateAsync(viewModel);
            }
        }

    }
}
