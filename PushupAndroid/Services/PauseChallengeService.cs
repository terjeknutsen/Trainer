using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ApplicationInterfaces;
using PushUp.Application;
using PushUp.Application.Commands;
using System.Threading.Tasks;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class PauseChallengeService : IntentService
    {
        private readonly IApplicationService challengeService;

        public PauseChallengeService()
        {
            challengeService = TinyIoC.TinyIoCContainer.Current.Resolve<IApplicationService>(nameof(ChallengeService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(PauseChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(PauseChallenge.Id)));
            var cmd = new PauseChallenge(challengeId);
            var task = challengeService.ExecuteCommand(cmd);
            task.ContinueWith(_ => BroadcastSuccess(challengeId), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(_ => BroadcastFailure(challengeId), TaskContinuationOptions.OnlyOnFaulted);
        }
        private void BroadcastSuccess(Guid challengeId)
        {
            var intent = new Intent(nameof(PauseChallengeSuccessReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(PauseChallenge.Id), challengeId.ToString());
            intent.PutExtra(nameof(PauseChallenge), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
        private void BroadcastFailure(Guid challengeId)
        {
            var intent = new Intent(nameof(PauseChallengeFailureReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(PauseChallenge.Id), challengeId.ToString());
            intent.PutExtra(nameof(PauseChallenge), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}