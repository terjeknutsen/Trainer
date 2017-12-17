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
            var nextWorkoutIntent = new Intent(this, typeof(WorkoutReminderService));
            StartService(nextWorkoutIntent);

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