using System;
using System.Globalization;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using no.trainer.personal.Interfaces;

namespace no.trainer.personal.Widgets
{
    [Register("no.trainer.personal.widgets.StepItemChallengeDate")]
    public class StepItemChallengeDate : LinearLayout
    {
        private TextView titleTextView;
        private TextView dateTextView;
        private Button selectDateButton;
        private string title;
        private ISelectDate dateSelector;

        public DateTime SelectedDate { get; private set; }

        public StepItemChallengeDate(Context context) :base(context)
        {
            Initialize(Tuple.Create<IAttributeSet, int>(null, 0));
        }
        public StepItemChallengeDate(Context context,IAttributeSet attrs) : base(context,attrs)
        {
            Initialize(Tuple.Create(attrs, 0));
        }
        public StepItemChallengeDate(Context context,IAttributeSet attrs,int defStyle):base(context,attrs,defStyle)
        {
            Initialize(Tuple.Create(attrs, defStyle));
        }
        private void Initialize(Tuple<IAttributeSet, int> tuple)
        {
            InitializeStyleAttributeProperties(tuple.Item1);
            InflateLayout();
            SetStyledAttributes();
            SetupEvents();
        }

        

        internal void SetDate(DateTime date)
        {
            SelectedDate = date;
            dateTextView.Text = date.ToString("d", CultureInfo.CurrentCulture);
        }

        private void InitializeStyleAttributeProperties(IAttributeSet attrs)
        {
            var nameSpace = "http://schemas.android.com/apk/lib/no.trainer.personal";
            var titleId = attrs.GetAttributeResourceValue(nameSpace, "step_title", -1);

            title = titleId!=-1 ? Context.GetString(titleId) :  attrs.GetAttributeValue(nameSpace, "step_title");

        }

        internal void SetDateSelector(ISelectDate dateSelector)
        {
            this.dateSelector = dateSelector;
        }

        private void InflateLayout()
        {
            var view = LayoutInflater.From(Context).Inflate(Resource.Layout.step_item_challenge_date, this,true);
            titleTextView = view.FindViewById<TextView>(Resource.Id.step_challenge_date_title);
            dateTextView = view.FindViewById<TextView>(Resource.Id.step_item_challenge_date);
            selectDateButton = view.FindViewById<Button>(Resource.Id.button_set_challenge_date);
        }
        private void SetStyledAttributes()
        {
            titleTextView.Text = title;
        }
        private void SetupEvents()
        {
            selectDateButton.Click += OnDateSelected;
        }

        private void OnDateSelected(object o, EventArgs e)
        {
           dateSelector.SelectDate(SelectedDate,SetDate);
        }
    }
}