using Android.App;
using Android.OS;
using no.trainer.personal.Activities.Steps;
using XamarinMaterialStepperLib;
using XamarinMaterialStepperLib.style;

namespace no.trainer.personal.Activities
{
    [Activity(Label = "AddChallengeActivity",Theme = "@style/Stepper.Theme")]
    public class AddChallengeActivity : DotStepper
    {
        int i = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            setTitle(Resources.GetString(Resource.String.add_challenge_title));
            addStep(CreateFragment(new ChallengeStep()));
            addStep(CreateFragment(new ChallengeDateStep()));
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