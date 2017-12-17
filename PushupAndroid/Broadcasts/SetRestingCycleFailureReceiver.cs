using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class SetRestingCycleFailureReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onFailure;

        public SetRestingCycleFailureReceiver(Action<Guid> onFailure)
        {
            this.onFailure = onFailure;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetRestingCycle));
            var challengeId = new Guid(bundle.GetString(nameof(SetRestingCycle.Id)));
            onFailure(challengeId);
        }
    }
}