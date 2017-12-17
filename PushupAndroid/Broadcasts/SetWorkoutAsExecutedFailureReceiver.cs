using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class SetWorkoutAsExecutedFailureReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onSetExecutedFailure;

        public SetWorkoutAsExecutedFailureReceiver(Action<Guid> onSetExecutedFailure)
        {
            this.onSetExecutedFailure = onSetExecutedFailure;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetWorkoutAsExecuted));
            onSetExecutedFailure(new Guid(bundle.GetString(nameof(SetWorkoutAsExecuted.Id))));

        }
    }
}