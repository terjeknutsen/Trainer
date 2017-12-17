using System;

namespace DomainEvents
{
    public sealed class WorkoutCreated : DomainEvent
    {
        public WorkoutCreated(WorkoutId id,WorkoutType type, int reps,DateTime eventDateTime):base(eventDateTime)
        {
            Id = id;
            Type = type;
            Reps = reps;
        }

        public WorkoutId Id { get; }
        public WorkoutType Type { get; }
        public int Reps { get; }
    }
}
