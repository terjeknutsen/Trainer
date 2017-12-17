using CoreDomain;
using PushUp.Application.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;
using StorageInterfaces;
using PushUp.Application.Commands;
using Domain.Identity;
using ApplicationInterfaces;
using ViewModels;
using PushUp.Application.Views;

namespace PushUp.Application
{
    public sealed class WorkoutService : ServiceBase<Workout>
    {
        public WorkoutService(IEventStore eventStore,IRepository<WorkoutViewModel> repository) : base(eventStore)
        {
            ViewStore = ViewStoreFactory.WorkoutViewModelStore(repository);
        }

        public override async Task ExecuteCommand(ICommand cmd)
        {
            await When((dynamic)cmd);
        }
        protected override Workout BuildAggregate(IEnumerable<IEvent> events)
        {
            return new Workout(events);
        }

        public async Task When(CreateWorkout cmd)
        {
            var workoutId = new WorkoutId(cmd.Id);
            var challengeId = new ChallengeId(cmd.ChallengeId);
            await Update(workoutId, w => w.Create(workoutId,challengeId, new CoreDomain.Types.WorkoutType(cmd.WorkoutType), cmd.Repetitions));
        }
        public async Task When(SetWorkoutAsExecuted cmd)
        {
            await Update(new WorkoutId(cmd.Id), w => w.Execute(DateTime.Now));
        }

        
    }
}
