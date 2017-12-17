using System;

namespace DomainEvents
{
    public sealed class MeasurementUnitChanged : DomainEvent
    {
        public MeasurementUnitChanged(MeasurementId id,string unit,DateTime eventDateTime) : base(eventDateTime)
        {
            Id = id;
            Unit = unit;
        }

        public MeasurementId Id { get; }
        public string Unit { get; }
    }
}
