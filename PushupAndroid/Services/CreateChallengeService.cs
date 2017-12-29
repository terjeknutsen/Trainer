using Android.App;
using Android.Content;
using TinyIoC;
using ApplicationInterfaces;
using PushUp.Application;
using PushUp.Application.Commands;
using System;
using System.Threading.Tasks;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class CreateChallengeService : IntentService
    {
        readonly IApplicationService challengeService;

        public CreateChallengeService()
        {
            var container = TinyIoCContainer.Current;
            challengeService = container.Resolve<IApplicationService>(nameof(ChallengeService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(CreateChallenge));
            var cmd = new CreateChallenge(
                Guid.NewGuid(),
                bundle.GetInt(nameof(CreateChallenge.DailyRepetitions)),
                bundle.GetString(nameof(CreateChallenge.Description)));
            var task = challengeService.ExecuteCommand(cmd);
            task.ContinueWith(_=> BroadcastSuccess());
            task.ContinueWith(_ => BroadcastFailure(),TaskContinuationOptions.OnlyOnFaulted);
        }

        void BroadcastSuccess()
        {
            var intent = new Intent(nameof(CreateChallengeSuccessReceiver));
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }

        private void BroadcastFailure()
        {
            var intent = new Intent(nameof(CreateChallengeFailureReceiver));
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}