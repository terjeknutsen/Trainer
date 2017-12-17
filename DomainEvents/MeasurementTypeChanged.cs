using System;

namespace DomainEvents
{
    public sealed class MeasurementTypeChanged : DomainEvent
    {
        public MeasurementTypeChanged(MeasurementId id,string type, DateTime eventDateTime) : base(eventDateTime)
        {
            Id = id;
            Type = type;
        }

        public MeasurementId Id { get; }
        public string Type { get; }
    }
}
