using System;
using Android.App;
using Android.Content;
using Android.OS;
using ApplicationInterfaces;
using PushUp.Application;
using PushUp.Application.Commands;
using System.Threading.Tasks;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class SetWorkoutExecutedService : IntentService
    {
        readonly IApplicationService workoutService;

        public SetWorkoutExecutedService()
        {
            workoutService = TinyIoC.TinyIoCContainer.Current.Resolve<IApplicationService>(nameof(WorkoutService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(SetWorkoutAsExecuted));
            var cmd = new SetWorkoutAsExecuted(new Guid(bundle.GetString(nameof(SetWorkoutAsExecuted.Id))));
            var task = workoutService.ExecuteCommand(cmd);
            task.ContinueWith(_ => BroadcastSuccess(cmd.Id),TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(_ => BroadcastFailure(cmd.Id), TaskContinuationOptions.OnlyOnFaulted);
        }

        private void BroadcastSuccess(Guid id)
        {
            var manager = (AlarmManager)GetSystemService(Context.AlarmService);
            var alarmIntent = new Intent(this,typeof(NextWorkoutReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, 2844, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var time = DateTime.Now.AddMinutes(1);
            var calendar = Java.Util.Calendar.Instance;
            calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
            calendar.Set(time.Year, time.Month - 1, time.Day, time.Hour, time.Minute, time.Second);

            AlarmManagerCompat.SetAlarmClock(manager, calendar.TimeInMillis, null, pendingIntent);

            var intent = new Intent(nameof(SetWorkoutAsExecutedSuccessReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetWorkoutAsExecuted.Id), id.ToString());
            intent.PutExtra(nameof(SetWorkoutAsExecuted), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
        private void BroadcastFailure(Guid id)
        {
            var intent = new Intent(nameof(SetWorkoutAsExecutedFailureReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetWorkoutAsExecuted.Id), id.ToString());
            intent.PutExtra(nameof(SetWorkoutAsExecuted), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}