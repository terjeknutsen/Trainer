using System;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V7.Widget;
using ViewModels;
using no.trainer.personal;
using Android.Support.V4.Content;
using Android.Graphics;
using System.Linq;

namespace PushupAndroid.Adapters
{
    class MainAdapter : RecyclerView.Adapter
    {
        List<IViewModel> views = new List<IViewModel>();
        private readonly Android.Content.Res.Resources resources;
        private readonly Action<Guid,Dictionary<int, bool>> setRestingCycle;
        private readonly Action<Guid> onPause;
        private readonly Action<Guid> onActivated;

        public override int ItemCount => views.Count;

        public Action<Guid> OnExecuted { get; }

        public MainAdapter(
            Android.Content.Res.Resources resources, 
            Action<Guid> onExecuted, 
            Action<Guid,Dictionary<int,bool>> setRestingCycle,
            Action<Guid> onPause,
            Action<Guid> onActivated)
        {
            this.resources = resources;
            OnExecuted = onExecuted;
            this.setRestingCycle = setRestingCycle;
            this.onPause = onPause;
            this.onActivated = onActivated;
        }
        internal void SetItems(IEnumerable<IViewModel> views)
        {
            this.views.Clear();
            this.views.AddRange(views);
            NotifyDataSetChanged();
        }

        internal void AddItems(IEnumerable<IViewModel> views)
        {
            this.views.AddRange(views);
            NotifyDataSetChanged();
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ChallengeItemHolder)
                SetValue(holder as ChallengeItemHolder, position);
            else if (holder is WorkoutItemHolder)
                SetValue(holder as WorkoutItemHolder, position);
            else
                SetValue(holder as MeasurementItemHolder, position);
        }

        private void SetValue(ChallengeItemHolder holder,int position)
        {
            holder.SetChallenge(views[position] as ChallengeViewModel);
        }
        private void SetValue(WorkoutItemHolder holder, int position)
        {
            holder.SetWorkoutItem(views[position] as WorkoutViewModel);
        }
        private void SetValue(MeasurementItemHolder holder,int position)
        {
            holder.SetViewModel(views[position] as MeasurementViewModel);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == 1)
               return CreateWorkoutCard(parent);
            if(viewType == 2)
            return CreateChallengeCard(parent);
            if (viewType == 3)
                return CreateMeasurementCard(parent);
            throw new InvalidOperationException($"ViewType not implemented");
        }
        private RecyclerView.ViewHolder CreateChallengeCard(ViewGroup parent)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_view_challenge, parent, false);
            var dayString = resources.GetString(Resource.String.day);
            var gainColor = ContextCompat.GetColor(parent.Context,Resource.Color.green);
            var lossColor = ContextCompat.GetColor(parent.Context,Resource.Color.red);
            var holder = new ChallengeItemHolder(dayString,GetColorFrom(gainColor),GetColorFrom(lossColor),layout,setRestingCycle,onPause,onActivated);
            return holder;
        }
        private RecyclerView.ViewHolder CreateWorkoutCard(ViewGroup parent)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_view_workout, parent, false);
            var holder = new WorkoutItemHolder(layout,OnExecuted);
            return holder;
        }
        private RecyclerView.ViewHolder CreateMeasurementCard(ViewGroup parent)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_view_measurement, parent, false);
            var holder = new MeasurementItemHolder(layout);
            return holder;
        }

        internal IViewModel GetById(Guid guid)
        {
            return views.First(v => v.Id == guid);
        }

        private Color GetColorFrom(int colorInt)
        {
            return Color.Rgb(Color.GetRedComponent(colorInt), Color.GetGreenComponent(colorInt), Color.GetBlueComponent(colorInt));
        }

        
        public override int GetItemViewType(int position)
        {
            if (views[position] is WorkoutViewModel)
                return 1;
            if (views[position] is ChallengeViewModel)
                return 2;
            if (views[position] is MeasurementViewModel)
                return 3;
            throw new InvalidOperationException($"{views[position].GetType()} in position {position} not implemented");
        }
    }
}