using System;
using Android.Content;
using PushUp.Application.Commands;

namespace no.trainer.personal.Broadcasts
{
    public sealed class PauseChallengeSuccessReceiver : BroadcastReceiver
    {
        private readonly Action<Guid> onChallengePaused;

        public PauseChallengeSuccessReceiver(Action<Guid> onChallengePaused)
        {
            this.onChallengePaused = onChallengePaused;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.GetBundleExtra(nameof(PauseChallenge));
            var challengeId = new Guid(bundle.GetString(nameof(PauseChallenge.Id)));
            onChallengePaused(challengeId);
        }
    }
}