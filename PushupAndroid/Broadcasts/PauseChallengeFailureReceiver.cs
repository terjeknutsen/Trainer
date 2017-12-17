using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class PauseChallengeFailureReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onFailure;

        public PauseChallengeFailureReceiver(Action<Guid> onFailure)
        {
            this.onFailure = onFailure;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(PauseChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(PauseChallenge.Id)));
            onFailure(challengeId);
        }
    }
}