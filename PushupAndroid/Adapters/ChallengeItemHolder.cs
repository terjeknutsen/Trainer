using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ViewModels;
using no.trainer.personal;
using System;
using System.Collections.Generic;
using PushupAndroid.Widgets;
using Android.Graphics;
using no.trainer.personal.Widgets;

namespace PushupAndroid.Adapters
{
    class ChallengeItemHolder : RecyclerView.ViewHolder
    {
        readonly string dayString;
        readonly Color gainColor;
        readonly Color lossColor;
        readonly Action<Guid,Dictionary<int, bool>> setRestingCycle;
        readonly Action<Guid> onPause;
        readonly Action<Guid> onActivated;
        CustomToggleButton monday;
        CustomToggleButton tuesday;
        CustomToggleButton wednesday;
        CustomToggleButton thursday;
        CustomToggleButton friday;
        CustomToggleButton saturday;
        CustomToggleButton sunday;

        public ChallengeItemHolder(
            string dayString,
            Color gainColor,
            Color lossColor ,
            View view, 
            Action<Guid,Dictionary<int, bool>> setRestingCycle, 
            Action<Guid> onPause,
            Action<Guid> onActivated) :base(view)
        {
            this.dayString = dayString;
            this.gainColor = gainColor;
            this.lossColor = lossColor;
            this.setRestingCycle = setRestingCycle;
            this.onPause = onPause;
            this.onActivated = onActivated;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            ItemView.FindViewById<PlayButton>(Resource.Id.play_pause_challenge).IsPaused += (o,e) =>  OnPauseChanged(e);
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.monday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.tuesday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.wednesday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.thursday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.friday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.saturday).CheckedChanged += delegate { SetRestingCycle(); };
            ItemView.FindViewById<CustomToggleButton>(Resource.Id.sunday).CheckedChanged += delegate { SetRestingCycle(); };
        }

        private void OnPauseChanged(bool paused)
        {
            if (paused)
                onPause(Model.Id);
            else
                onActivated(Model.Id);
        }

        private void SetRestingCycle()
        {
            var currentRestingCycle = new Dictionary<int, bool>
            {
                {1,monday.IsChecked },
                {2,tuesday.IsChecked },
                {3,wednesday.IsChecked },
                {4,thursday.IsChecked },
                {5,friday.IsChecked },
                {6,saturday.IsChecked },
                {7,sunday.IsChecked }
            };
            setRestingCycle(Model.Id,currentRestingCycle);
        }

        public ChallengeViewModel Model { get; private set; }
   

        public void SetChallenge(ChallengeViewModel model)
        {
            Model = model;
            ItemView.FindViewById<TextView>(Resource.Id.challenge_days_left).Text = $"{dayString} {model.DaysLeft}";
            ItemView.FindViewById<TextView>(Resource.Id.challenge_description).Text = model.Desciption;
            ItemView.FindViewById<TextView>(Resource.Id.challenge_total_repetitions).Text = model.TotalRepetitions.ToString();
            
            
            monday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.monday);
            tuesday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.tuesday);
            wednesday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.wednesday);
            thursday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.thursday);
            friday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.friday);
            saturday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.saturday);
            sunday = ItemView.FindViewById<CustomToggleButton>(Resource.Id.sunday);

            SetSelectedWeekdays(model);
            SetMeasurements(model.Measurements);
        }

        private void ResetChallenge(bool isPaused)
        {
            ItemView.FindViewById<PlayButton>(Resource.Id.play_pause_challenge).IsPaused -= (o, e) => ResetChallenge(e);
            Model.Pause(isPaused);
            SetChallenge(Model);
        }

        private void DeselectAllWeekdays()
        {
            sunday.SetState(false);
            saturday.SetState(false);
            friday.SetState(false);
            thursday.SetState(false);
            wednesday.SetState(false);
            tuesday.SetState(false);
            monday.SetState(false);
        }

        private void SetSelectedWeekdays(ChallengeViewModel model)
        {
            sunday.SetState(model.ActiveSunday);
            saturday.SetState(model.ActiveSaturday);
            friday.SetState(model.ActiveFriday);
            thursday.SetState(model.ActiveThursday);
            wednesday.SetState(model.ActiveWednesday);
            tuesday.SetState(model.ActiveTuesday);
            monday.SetState(model.ActiveMonday);
        }


        private void SetMeasurements(IEnumerable<Tuple<string, double, string, double>> measurements)
        {
            if (measurements == null) return;
            TableLayout tableLayout = ItemView.FindViewById<TableLayout>(Resource.Id.highlight_wrapper);
            tableLayout.RemoveAllViews();
            foreach(var tuple in measurements)
            {
                var row = LayoutInflater.From(ItemView.Context).Inflate(Resource.Layout.HighlightItemRow, null);
                row.FindViewById<TextView>(Resource.Id.description).Text = tuple.Item1;
                row.FindViewById<TextView>(Resource.Id.current_measurement).Text = $"{tuple.Item2}{tuple.Item3}";
                var gainLoss = row.FindViewById<TextView>(Resource.Id.gain_loss);
                gainLoss.Text = $"{Math.Abs(tuple.Item4)}{tuple.Item3}";
                gainLoss.SetTextColor((tuple.Item4 > 0 ? gainColor : lossColor));
                tableLayout.AddView(row);
            }
          
        }
    }
}