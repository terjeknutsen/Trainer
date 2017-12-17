using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Util;
using no.trainer.personal;
using Android.Support.V4.Content;

namespace PushupAndroid.Widgets
{
    [Register("pushupandroid.widgets.CustomToggleButton")]
    public class CustomToggleButton : Button
    {
        private bool isChecked;
        public event EventHandler<bool> CheckedChanged;
        public CustomToggleButton(Context context) : base(context)
        {
            Initialize(Tuple.Create<IAttributeSet, int>(null, 0));
        }
        public CustomToggleButton(Context context, IAttributeSet attrs) : base(context,attrs)
        {
            Initialize(Tuple.Create(attrs, 0));
        }

        public CustomToggleButton(Context context, IAttributeSet attrs,int defStyle):base(context,attrs,defStyle)
        {
            Initialize(Tuple.Create(attrs, defStyle));
        }
        public bool IsChecked => isChecked;
        public void SetState(bool state)
        {
            isChecked = state;
            SetViewState();
        }

        private void Initialize(Tuple<IAttributeSet, int> tuple)
        {
            InitializeStyleAttributeProperties(tuple.Item1);
            Click += delegate 
            {
                SetState(!isChecked);
                CheckedChanged?.Invoke(this, isChecked);
            };   
        }

        private void InitializeStyleAttributeProperties(IAttributeSet attrs)
        {
            var nameSpace = "http://schemas.android.com.apk/res-auto";
            isChecked = attrs.GetAttributeBooleanValue(nameSpace, "isSelected", false);
            SetViewState();
        }

        private void SetViewState()
        {
            if(isChecked)
            {
                Background = ContextCompat.GetDrawable(Context, Resource.Drawable.week_day_button_selected);
                PaintFlags = Android.Graphics.PaintFlags.LinearText;
            }
            else
            {
                Background = ContextCompat.GetDrawable(Context,Resource.Drawable.week_day_button_unselected);
                PaintFlags = Android.Graphics.PaintFlags.StrikeThruText;
            }
            Invalidate();
            RequestLayout();
        }
    }
}