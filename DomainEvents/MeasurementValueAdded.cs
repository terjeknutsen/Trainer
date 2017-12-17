using System;

namespace DomainEvents
{
    public sealed class MeasurementValueAdded : DomainEvent
    {
        public MeasurementValueAdded(double value,DateTime eventDateTime) : base(eventDateTime)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
