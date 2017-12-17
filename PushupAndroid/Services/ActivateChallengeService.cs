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
using PushUp.Application.Commands;
using ApplicationInterfaces;
using PushUp.Application;
using System.Threading.Tasks;
using Android.Support.V4.Content;
using no.trainer.personal.Broadcasts;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class ActivateChallengeService : IntentService
    {
        readonly IApplicationService challengeService;

        public ActivateChallengeService()
        {
            challengeService = TinyIoC.TinyIoCContainer.Current.Resolve<IApplicationService>(nameof(ChallengeService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(ActivateChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(ActivateChallenge.Id)));
            var cmd = new ActivateChallenge(challengeId);
            var task = challengeService.ExecuteCommand(cmd);
            task.ContinueWith(_ => BroadcastSuccess(challengeId),TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(_ => BroadcastFailure(challengeId), TaskContinuationOptions.OnlyOnFaulted);
        }

        private void BroadcastSuccess(Guid challengeId)
        {
            var intent = new Intent(nameof(ActivateChallengeSuccessReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(ActivateChallenge.Id), challengeId.ToString());
            intent.PutExtra(nameof(ActivateChallenge), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }

        private void BroadcastFailure(Guid challengeId)
        {
            var intent = new Intent(nameof(ActivateChallengeFailureReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(ActivateChallenge.Id), challengeId.ToString());
            intent.PutExtra(nameof(ActivateChallenge), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}