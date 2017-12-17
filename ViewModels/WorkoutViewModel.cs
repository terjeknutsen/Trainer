using System;

namespace ViewModels
{
    public sealed class WorkoutViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string WorkoutType { get; set; }
        public int Reps { get; set; }
        public bool Executed { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
