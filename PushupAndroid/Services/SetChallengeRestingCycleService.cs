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
using Android.Support.V4.Content;
using no.trainer.personal.Broadcasts;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class SetChallengeRestingCycleService : IntentService
    {
        readonly IApplicationService challengeService;

        public SetChallengeRestingCycleService()
        {
            challengeService = TinyIoC.TinyIoCContainer.Current.Resolve<IApplicationService>(nameof(ChallengeService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetRestingCycle));
            var challengeId = new Guid(bundle.GetString(nameof(SetRestingCycle.Id)));
            var boolArray = bundle.GetBooleanArray(nameof(SetRestingCycle.RestingCycle));
            var cmd = new SetRestingCycle(challengeId, boolArray[0], boolArray[1], boolArray[2], boolArray[3], boolArray[4], boolArray[5], boolArray[6]);
            var task = challengeService.ExecuteCommand(cmd);
            task.ContinueWith(_ => BroadcastSuccess(challengeId), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(_ => BroadcastFailure(challengeId), TaskContinuationOptions.OnlyOnFaulted);
        }

        private void BroadcastSuccess(Guid challengeId)
        {
            var intent = new Intent(nameof(SetRestingCycleSuccessReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetRestingCycle.Id), challengeId.ToString());
            intent.PutExtra(nameof(SetRestingCycle), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }

        private void BroadcastFailure(Guid challengeId)
        {
            var intent = new Intent(nameof(SetRestingCycleFailureReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetRestingCycle.Id), challengeId.ToString());
            intent.PutExtra(nameof(SetRestingCycle), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}