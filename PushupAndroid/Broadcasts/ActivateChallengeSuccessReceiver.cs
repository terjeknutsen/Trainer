using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class ActivateChallengeSuccessReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onSuccess;

        public ActivateChallengeSuccessReceiver(Action<Guid> onSuccess)
        {
            this.onSuccess = onSuccess;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(ActivateChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(ActivateChallenge.Id)));
            onSuccess(challengeId);
        }
    }
}