using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace no.trainer.personal.Services
{
    [Service]
    public sealed class WorkoutReminderService : IntentService
    {
        private NotificationManager notifier;

        public override void OnCreate()
        {
            base.OnCreate();
            notifier = (NotificationManager)GetSystemService(Context.NotificationService);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            var intentToLaunch = new Intent(ApplicationContext, Class);
            var pendingIntent = PendingIntent.GetActivity(ApplicationContext, 0, intentToLaunch, 0);
            var builder = new NotificationCompat.Builder(ApplicationContext);
            builder.SetTicker("Builder workout reminder");
            builder.SetSmallIcon(Android.Resource.Drawable.StatNotifyMore);
            builder.SetContentTitle("Builder workout reminder");
            builder.SetContentText("Friman er kul");
            builder.SetContentIntent(pendingIntent);
            builder.SetAutoCancel(true);
            var notify = builder.Build();
            notifier.Notify(8471, notify);
        }

        class TimerCallback
        {
            private NotificationManager notifier;
            private Notification notify;

            public TimerCallback(NotificationManager notifier, Notification notify)
            {
                this.notifier = notifier;
                this.notify = notify;
            }
            public void SendNotification(System.Object info)
            {
                notifier.Notify(8471, notify);
            }
        }
    }
}