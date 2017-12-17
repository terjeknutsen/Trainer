using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class ActivateChallengeFailureReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onFailure;

        public ActivateChallengeFailureReceiver(Action<Guid> onFailure)
        {
            this.onFailure = onFailure;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(ActivateChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(ActivateChallenge.Id)));
            onFailure(challengeId);
        }
    }
}