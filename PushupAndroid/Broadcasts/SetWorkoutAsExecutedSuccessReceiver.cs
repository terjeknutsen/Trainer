using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class SetWorkoutAsExecutedSuccessReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onExecuted;

        public SetWorkoutAsExecutedSuccessReceiver(Action<Guid> onExecuted)
        {
            this.onExecuted = onExecuted;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetWorkoutAsExecuted));
            onExecuted(new Guid(bundle.GetString(nameof(SetWorkoutAsExecuted.Id))));
        }
    }
}