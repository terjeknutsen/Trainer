using Android.Content;
using no.trainer.personal.Services;

namespace no.trainer.personal.Broadcasts
{
    [BroadcastReceiver]
    
    public sealed class NextWorkoutReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var serviceIntent = new Intent(context,typeof(WorkoutReminderService));
            context.StartService(serviceIntent);
        }
    }
}