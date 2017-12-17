using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class SetRestingCycleSuccessReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onSuccess;

        public SetRestingCycleSuccessReceiver(Action<Guid> onSuccess)
        {
            this.onSuccess = onSuccess;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetRestingCycle));
            var challengeId = new Guid(bundle.GetString(nameof(SetRestingCycle.Id)));
            onSuccess(challengeId);
        }
    }
}  