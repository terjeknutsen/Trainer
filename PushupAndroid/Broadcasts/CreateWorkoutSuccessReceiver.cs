using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class CreateWorkoutSuccessReceiver : BroadcastReceiver
    {
        readonly Action<Guid> onWorkoutCreated;

        public CreateWorkoutSuccessReceiver(Action<Guid> onWorkoutCreated)
        {
            this.onWorkoutCreated = onWorkoutCreated;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(CreateWorkout));
            onWorkoutCreated(new Guid(bundle.GetString(nameof(CreateWorkout.Id))));
        }
    }
}