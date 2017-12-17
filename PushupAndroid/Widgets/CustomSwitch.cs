using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Support.V7.Widget;
using System;
using no.trainer.personal;
using Android.Runtime;
using Android.Graphics;

namespace PushupAndroid.Widgets
{
    [Register("pushupandroid.widgets.CustomSwitch")]
    public class CustomSwitch : LinearLayout
    {
        private string text;
        private bool isChecked;
        private SwitchCompat switchCompat;
        private TextView textView;


        public CustomSwitch(Context context) : base(context)
        {
            Initialize(context,Tuple.Create<IAttributeSet, int>(null, 0));
        }
        public CustomSwitch(Context context, IAttributeSet attrs)
        :base(context,attrs)
        {
            Initialize(context,Tuple.Create(attrs, 0));   
        }
        public CustomSwitch(Context context, IAttributeSet attrs, int defStyle) : base(context,attrs,defStyle)
        {
            Initialize(context,Tuple.Create(attrs, defStyle));
        }

        void Initialize(Context context,Tuple<IAttributeSet,int> values)
        {
            InitializeLinearLayoutProperties();
            InitializeStyleAttributeProperties(context,values.Item1);
            InflateLayout();
            UpdateDisplayedSwitch();
        }

        private void InitializeLinearLayoutProperties()
        {
            SetGravity(GravityFlags.CenterVertical);
            Orientation = Android.Widget.Orientation.Horizontal;
        }

        private void InitializeStyleAttributeProperties(Context context,IAttributeSet attrs)
        {
            //var nameSpace = "http://schemas.android.com.apk/res-auto";
            //var selectedId = attrs.GetAttributeResourceValue(nameSpace, "day_selected", -1);
            //var onId = attrs.GetAttributeResourceValue(nameSpace, "text_day_on", -1);
            //var offId = attrs.GetAttributeResourceValue(nameSpace, "text_day_off", -1);
            //isChecked = attrs.GetAttributeBooleanValue(nameSpace, "day_selected", false);
            //text = onId != -1 ? context.GetString(onId) : attrs.GetAttributeValue(nameSpace, "text_day_on");
        }

        private void InflateLayout()
        {
            var view = LayoutInflater.From(Context).Inflate(Resource.Layout.custom_switch, this);
            switchCompat = view.FindViewById<SwitchCompat>(Resource.Id.switch_compat);
            textView = view.FindViewById<TextView>(Resource.Id.switch_compat_text);
            switchCompat.CheckedChange += delegate { SetChecked(switchCompat.Checked); };
        }

        private void UpdateDisplayedSwitch()
        {
            switchCompat.Checked = isChecked;
            textView.Text = text;
            if (isChecked)
            {
                textView.PaintFlags = PaintFlags.LinearText;
            }
            else
            {
                textView.PaintFlags = PaintFlags.StrikeThruText;
            }
        }

        public void SetChecked(bool isChecked)
        {
            this.isChecked = isChecked;
            UpdateDisplayedSwitch();
            Invalidate();
            RequestLayout();
        }
        public bool IsChecked => isChecked;
   }
}