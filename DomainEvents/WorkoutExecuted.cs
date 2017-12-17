using System;

namespace DomainEvents
{
    public sealed class WorkoutExecuted : DomainEvent
    {
        public WorkoutExecuted(WorkoutId id,DateTime eventDateTime):base(eventDateTime)
        {
            Id = id;
        }

        public WorkoutId Id { get; }
    }
}
