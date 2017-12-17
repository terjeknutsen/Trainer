using System;

namespace DomainEvents
{
    public sealed class DailyRepetitionsChanged : DomainEvent
    {
        public  DailyRepetitionsChanged(int repetitions,DateTime eventDateTime) : base(eventDateTime)
        {
            Repetitions = repetitions;
        }

        public int Repetitions { get; }
    }
}
