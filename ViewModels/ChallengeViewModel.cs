using System;
using System.Collections.Generic;

namespace ViewModels
{
    public sealed class ChallengeViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartDate { get; set; }
        public int DailyRepetitions { get; set; }
        public string Desciption { get; set; }
        public string WorkoutDescription { get; set; } 
        public bool IsPaused { get; set; }
        public DateTime PausedDateTime { get; set; }
        public TimeSpan PausedDuration { get; set; }
        private bool monday;
        private bool tuesday;
        private bool wednesday;
        private bool thursday;
        private bool friday;
        private bool saturday;
        private bool sunday;
        public bool ActiveMonday
        {
            get
            {
                return IsPaused ? false : monday;
            }
            set
            {
                monday = value;
            }
        }
        public bool ActiveTuesday
        {
            get
            {
                return IsPaused ? false : tuesday;
            }
            set
            {
                tuesday = value;
            }
        }
        public bool ActiveWednesday
        {
            get { return IsPaused ? false : wednesday; } set { wednesday = value; }
        }
        public bool ActiveThursday
        {
            get { return IsPaused ? false : thursday; }
            set { thursday = value; }
        }
        public bool ActiveFriday
        {
            get { return IsPaused ? false : friday; }
            set { friday = value; }
        }
        public bool ActiveSaturday
        {
            get { return IsPaused ? false : saturday; }
            set { saturday = value; }
        }
        public bool ActiveSunday
        {
            get { return IsPaused ? false : sunday; }
            set { sunday = value; }
        }
        public int DaysLeft => (DateTime.Now - StartDate).Days;
        public int TotalRepetitions { get; set; }
        public IList<Tuple<string,double,string,double>> Measurements { get; set; }


        public void Pause(bool isPaused)
        {
            IsPaused = isPaused;
            if (IsPaused)
            {
                PausedDateTime = DateTime.Now;
            }
            else
            {
                PausedDuration += DateTime.Now - PausedDateTime;
            }
        }
    }
}
