using System.Collections.Generic;
using CoreDomain;
using DomainInterfaces;
using PushUp.Application.Base;
using PushUp.Application.Commands;
using StorageInterfaces;
using Domain.Identity;
using System.Threading.Tasks;
using System;
using ApplicationInterfaces;
using ViewModels;
using PushUp.Application.Views;
using CoreDomain.Enteties;

namespace PushUp.Application
{
    public class ChallengeService : ServiceBase<Challenge>
    {
        public ChallengeService(IEventStore eventStore,IRepository<ChallengeViewModel> repository):base(eventStore)
        {
            ViewStore = ViewStoreFactory.ChallengeViewModelStore(repository);
        }

        protected override Challenge BuildAggregate(IEnumerable<IEvent> events)
        {
            return new Challenge(events);
        }

        public override async Task ExecuteCommand(ICommand cmd)
        {
            await When((dynamic)cmd);
        }

        async Task When(CreateChallenge cmd)
        {
            var challengeId = new ChallengeId(cmd.Id);
            await Update(challengeId, c => c.Create(challengeId,cmd.DailyRepetitions,cmd.Description));  
        }
        async Task When(SetChallengeDuration cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.SetDuration(cmd.Duration, DateTime.Now));
        }
        async Task When(SetChallengeDescription cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.SetDescription(cmd.Description, DateTime.Now));
        }
        async Task When(SetDailyRepetitions cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.SetDailyRepetitions(cmd.Repetitions, DateTime.Now));
        }
        async Task When(SetRestingCycle cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.SetRestingCycle(cmd.RestingCycle, DateTime.Now));
        }
        async Task When(PauseChallenge cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.Pause(DateTime.Now));
        }
        async Task When(ActivateChallenge cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.Activate(DateTime.Now));
        }
        async Task When(SetWorkoutSchedule cmd)
        {
            await Update(new ChallengeId(cmd.Id), c => c.SetWorkoutSchedule(new WorkoutSchedule { Schedule = cmd.Schedule }, DateTime.Now));
        }
    }
}
