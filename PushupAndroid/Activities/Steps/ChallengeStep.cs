using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using XamarinMaterialStepperLib;

namespace no.trainer.personal.Activities.Steps
{
    
    public class ChallengeStep : AbstractStep, 
        IControlFocus,
        IUpdateDailyRepetitions,
        IAdjustPossibleDailyRepetitions
    {
        private int i = 1;
        private TextInputEditText editTextDescription;
        private EditText editTextDailyRepetitions;
        private SeekBar seekBarDailyRepetitions;
        private readonly static System.String CLICK = "click";

        public bool HasFocus => !SeekbarActivated;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                i = savedInstanceState.GetInt(CLICK, i);
            return inflater.Inflate(Resource.Layout.step_challenge, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            editTextDescription = view.FindViewById<TextInputEditText>(Resource.Id.edit_text_challenge_description);
            editTextDailyRepetitions = view.FindViewById<EditText>(Resource.Id.edit_text_challenge_daily_repetitions);
            seekBarDailyRepetitions = view.FindViewById<SeekBar>(Resource.Id.seekbar_challenge_daily_repetitions);
            base.OnViewCreated(view, savedInstanceState);
        }
        public override void OnStart()
        {
            editTextDailyRepetitions.AddTextChangedListener(new DailyRepetitionWatcher(this,this,this));
            seekBarDailyRepetitions.ProgressChanged += OnDailyRepetitionsChanged;
            seekBarDailyRepetitions.StartTrackingTouch += delegate { SeekbarActivated = true; };
            seekBarDailyRepetitions.StopTrackingTouch += delegate { SeekbarActivated = false; };
            base.OnStart();
        }
        private bool SeekbarActivated { get; set; }
        private void OnDailyRepetitionsChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if(SeekbarActivated)
            editTextDailyRepetitions.Text = ""+e.Progress;
        }

        public override void OnNotPermission()
        {
            base.OnNotPermission();
            Console.WriteLine("Not Permission");
            Toast.MakeText(Activity, "Not Permission", ToastLength.Short).Show();


            //Now Allow For Test
            PermissionNext = true;
        }


        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt(CLICK, i);
        }

        public override string name()
        {
            return "Tab " + Arguments.GetInt("position", 0);
        }

        public override bool isOptional()
        {
            return false;
        }


        public override void onStepVisible()
        {
        }

        public override void onNext()
        {
            Console.WriteLine("onNext");
        }

        public override void onPrevious()
        {
            Console.WriteLine("onPrevious");
        }

        public override System.String optional()
        {
            return "You can skip";
        }

        public override bool nextIf()
        {
            return i > 1;
        }

        public override System.String error()
        {
            return "<b>You must click!</b> <small>this is the condition!</small>";
        }

        public void SetValue(int value)
        {
            seekBarDailyRepetitions.Progress = value;
        }

        public void SetMaxDailyRepetitions(int currentValue)
        {
            var max = currentValue * 1.5;
            seekBarDailyRepetitions.Max = (int)max;
        }
    }
    interface IControlFocus
    {
        bool HasFocus { get; }
    }
    interface IUpdateDailyRepetitions
    {
        void SetValue(int value);
    }
    interface IAdjustPossibleDailyRepetitions
    {
        void SetMaxDailyRepetitions(int currentValue);
    }
    class DailyRepetitionWatcher : Java.Lang.Object, ITextWatcher
    {
        private readonly IControlFocus focusController;
        private readonly IUpdateDailyRepetitions dailyRepetitionsUpdater;
        private readonly IAdjustPossibleDailyRepetitions maxDailyRepetitionsAdjuster;

        public DailyRepetitionWatcher(IControlFocus focusController, IUpdateDailyRepetitions dailyRepetitionsUpdater,IAdjustPossibleDailyRepetitions maxDailyRepetitionsAdjuster)
        {
            this.focusController = focusController;
            this.dailyRepetitionsUpdater = dailyRepetitionsUpdater;
            this.maxDailyRepetitionsAdjuster = maxDailyRepetitionsAdjuster;
        }

        public void AfterTextChanged(IEditable s)
        {
            if (!focusController.HasFocus) return;
            int.TryParse(s.ToString(), out int result);
            if (result > 0)
                maxDailyRepetitionsAdjuster.SetMaxDailyRepetitions(result);
            dailyRepetitionsUpdater.SetValue(result);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }
    }
}