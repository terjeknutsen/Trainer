using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using no.trainer.personal;
using System;
using ViewModels;

namespace PushupAndroid.Adapters
{
    internal class WorkoutItemHolder : RecyclerView.ViewHolder
    {
        public WorkoutItemHolder(View layout,Action<Guid> onExecuted):base(layout)
        {
            ItemView.FindViewById<Button>(Resource.Id.executedBtn).Click += delegate { onExecuted(Model.Id); };
        }

        public WorkoutViewModel Model { get; private set; }

        public void SetWorkoutItem(WorkoutViewModel model)
        {
            Model = model;
            ItemView.FindViewById<TextView>(Resource.Id.workout_reps).Text = model.Reps.ToString();
            ItemView.FindViewById<TextView>(Resource.Id.workout_description).Text = model.WorkoutType;
        }
    }
}