using System;
using Android.OS;
using Android.Views;
using XamarinMaterialStepperLib;

namespace no.trainer.personal.Activities.Steps
{
    public class WorkoutStep : AbstractStep
    {
        int i = 3;
        readonly static System.String CLICK = "click";
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.step_workout, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }
        public override void OnStart()
        {
            base.OnStart();
        }
        public override void OnResume()
        {
            base.OnResume();
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
        public override void OnNotPermission()
        {
            base.OnNotPermission();
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
    }
}