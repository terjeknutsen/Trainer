﻿using System;
using Android.OS;
using Android.Views;
using no.trainer.personal.Interfaces;
using no.trainer.personal.Widgets;
using XamarinMaterialStepperLib;

namespace no.trainer.personal.Activities.Steps
{
    public class ChallengeDateStep : AbstractStep
    {
        int i = 2;
        private StepItemChallengeDate stepStart;
        private StepItemChallengeDate stepEnd;
        private readonly static System.String CLICK = "click";
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                i = savedInstanceState.GetInt(CLICK,i);
           return inflater.Inflate(Resource.Layout.step_challenge_date, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            stepStart = view.FindViewById<StepItemChallengeDate>(Resource.Id.step_challenge_date_start);
            stepEnd = view.FindViewById<StepItemChallengeDate>(Resource.Id.step_challenge_date_end);
            PermissionNext = true;
            stepStart.SetDate(DateTime.Now);
            stepEnd.SetDate(DateTime.Now.AddMonths(1));
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var dateSelector = Activity as ISelectDate;
            if (dateSelector == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(ISelectDate)}");
            stepStart.SetDateSelector(dateSelector);
            stepEnd.SetDateSelector(dateSelector);
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