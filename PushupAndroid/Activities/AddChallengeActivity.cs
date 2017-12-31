using System;
using Android.App;
using Android.OS;
using no.trainer.personal.Activities.Steps;
using no.trainer.personal.Interfaces;
using XamarinMaterialStepperLib;
using XamarinMaterialStepperLib.style;

namespace no.trainer.personal.Activities
{
    [Activity(Label = "AddChallengeActivity",Theme = "@style/Stepper.Theme.DeepOrange")]
    public class AddChallengeActivity : DotStepper, ISelectDate
    {
        int i = 1;

        public void SelectDate(DateTime dateTime,Action<DateTime> onDateSelected)
        {
            var dialog = new DatePickerDialog(this,(o,e)=> OnDateSelected(e,onDateSelected), dateTime.Year, dateTime.Month-1, dateTime.Day);
            dialog.Show();
        }
        void OnDateSelected(DatePickerDialog.DateSetEventArgs eventArgs,Action<DateTime> onDateSelected)
        {
            var date = new DateTime(eventArgs.Year, eventArgs.Month + 1, eventArgs.DayOfMonth);
            onDateSelected(date);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            setTitle(Resources.GetString(Resource.String.add_challenge_title));
            addStep(CreateFragment(new ChallengeStep()));
            addStep(CreateFragment(new ChallengeDateStep()));
            addStep(CreateFragment(new WorkoutStep()));
            base.OnCreate(savedInstanceState);
        }

        private AbstractStep CreateFragment(AbstractStep fragment)
        {
            var bundle = new Bundle();
            bundle.PutInt("position", i++);
            fragment.Arguments = bundle;
            return fragment;
        }
    }
}