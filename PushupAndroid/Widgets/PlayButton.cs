using System;
using Android.Content;
using Android.Widget;
using Android.Util;
using Android.Support.V4.Content;
using Android.Runtime;

namespace no.trainer.personal.Widgets
{
    [Register("no.trainer.personal.widgets.PlayButton")]
    public class PlayButton : ImageButton
    {
        private bool isPaused;
        public event EventHandler<bool> IsPaused;
        protected virtual void OnIsPaused(EventArgs e)
        {
            IsPaused?.Invoke(this, isPaused);
        }
        public PlayButton(Context context) : base(context)
        {
            Initialize(Tuple.Create<IAttributeSet, int>(null, 0));
        }
        public PlayButton(Context context,IAttributeSet attrs) : base(context,attrs)
        {
            Initialize(Tuple.Create(attrs, 0));
        }
        public PlayButton(Context context,IAttributeSet attrs, int defStyle):base(context,attrs,defStyle)
        {
            Initialize(Tuple.Create(attrs, defStyle));
        }
        public bool IsPlaying => isPaused;

        private void Initialize(Tuple<IAttributeSet, int> tuple)
        {
            InitializeStyleAttributeProperties(tuple.Item1);
            Click += delegate { SetState(!isPaused); };
        }

        private void InitializeStyleAttributeProperties(IAttributeSet attrs)
        {
            var nameSpace = "http://schemas.android.com.apk/res-auto";
            isPaused = attrs.GetAttributeBooleanValue(nameSpace, "isPaused", false);
            SetViewState();
        }

        private void SetState(bool state)
        {
            isPaused = state;
            SetViewState();
            OnIsPaused(default(EventArgs));
        }

        private void SetViewState()
        {
            if (isPaused)
            {
                SetImageDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.ic_play_circle_outline));
            }
            else
            {
                SetImageResource(Resource.Drawable.ic_pause_circle_outline);
            }
            Invalidate();
            RequestLayout();
        }
    }
}