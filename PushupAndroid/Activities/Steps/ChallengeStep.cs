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
        IAdjustPossibleDailyRepetitions,
        IVerifyChallenge
    {
        private int i = 1;
        private TextInputEditText editTextDescription;
        private TextInputLayout textInputLayoutChallengeDescription;
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
            textInputLayoutChallengeDescription = view.FindViewById<TextInputLayout>(Resource.Id.text_input_layout_challenge_description);
            editTextDailyRepetitions = view.FindViewById<EditText>(Resource.Id.edit_text_challenge_daily_repetitions);
            seekBarDailyRepetitions = view.FindViewById<SeekBar>(Resource.Id.seekbar_challenge_daily_repetitions);
            PermissionNext = true;
            base.OnViewCreated(view, savedInstanceState);
        }
        public override void OnStart()
        {
            editTextDailyRepetitions.AddTextChangedListener(new DailyRepetitionWatcher(this,this,this,this));
            editTextDescription.AfterTextChanged += (o,e) => VerifyChallenge();
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
            
            if (string.IsNullOrEmpty(editTextDescription.Text) || string.IsNullOrWhiteSpace(editTextDescription.Text))
            {
                textInputLayoutChallengeDescription.Error = Resources.GetString(Resource.String.error_challenge_description);
            }
            else
            {
                textInputLayoutChallengeDescription.Error = null;
            }

            base.OnNotPermission();
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

        public override bool nextIf()
        {
            return i > 1;
        }

        public override System.String error()
        {
            return Resources.GetString(Resource.String.step_challenge_error);
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

        public void VerifyChallenge()
        {
            if (string.IsNullOrEmpty(editTextDescription.Text) || string.IsNullOrWhiteSpace(editTextDailyRepetitions.Text))
            {
                textInputLayoutChallengeDescription.Error = Resources.GetString(Resource.String.error_challenge_description);
                i = 1;
                return;
            }
            else
                textInputLayoutChallengeDescription.Error = null;
            if (!int.TryParse(editTextDailyRepetitions.Text, out int result))
            {
                i = 1;
                return;
            }

            if (i == 2) return;

            i = 2;
            mStepper.getExtras().PutInt(CLICK, i);
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
    interface IVerifyChallenge
    {
        void VerifyChallenge();
    }
    class DailyRepetitionWatcher : Java.Lang.Object, ITextWatcher
    {
        private readonly IControlFocus focusController;
        private readonly IUpdateDailyRepetitions dailyRepetitionsUpdater;
        private readonly IAdjustPossibleDailyRepetitions maxDailyRepetitionsAdjuster;
        private readonly IVerifyChallenge challengeVerifier;

        public DailyRepetitionWatcher(
            IControlFocus focusController, 
            IUpdateDailyRepetitions dailyRepetitionsUpdater,
            IAdjustPossibleDailyRepetitions maxDailyRepetitionsAdjuster,
            IVerifyChallenge challengeVerifier)
        {
            this.focusController = focusController;
            this.dailyRepetitionsUpdater = dailyRepetitionsUpdater;
            this.maxDailyRepetitionsAdjuster = maxDailyRepetitionsAdjuster;
            this.challengeVerifier = challengeVerifier;
        }

        public void AfterTextChanged(IEditable s)
        {
            if (!focusController.HasFocus) return;
            int.TryParse(s.ToString(), out int result);
            if (result > 0)
                maxDailyRepetitionsAdjuster.SetMaxDailyRepetitions(result);
            dailyRepetitionsUpdater.SetValue(result);
            challengeVerifier.VerifyChallenge();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }
    }
}