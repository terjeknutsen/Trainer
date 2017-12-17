using System;
using Android.App;
using Android.Content;
using PushUp.Application.Commands;
using ApplicationInterfaces;
using PushUp.Application;
using Android.OS;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class CreateWorkoutService : IntentService
    {
        readonly IApplicationService workoutService;

        public CreateWorkoutService()
        {
            workoutService = TinyIoC.TinyIoCContainer.Current.Resolve<IApplicationService>(nameof(WorkoutService));
        }
        protected override void OnHandleIntent(Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(CreateWorkout));
            var cmd = new CreateWorkout(Guid.NewGuid(),new Guid(bundle.GetString(nameof(CreateWorkout.ChallengeId))), bundle.GetString(nameof(CreateWorkout.WorkoutType)), bundle.GetInt(nameof(CreateWorkout.Repetitions)));
            var task = workoutService.ExecuteCommand(cmd);
            task.ContinueWith(_ => OnSuccess(cmd.Id));
            task.ContinueWith(_ => OnFailure());
        }

        void OnSuccess(Guid id)
        {
            var intent = new Intent(nameof(CreateWorkoutSuccessReceiver));
            var bundle = new Bundle();
            bundle.PutString(nameof(CreateWorkout.Id), id.ToString());
            intent.PutExtra(nameof(CreateWorkout), bundle);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
        void OnFailure()
        {

        }
    }
}