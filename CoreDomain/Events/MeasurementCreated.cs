using CoreDomain.Base;
using CoreDomain.Identity;
using System;

namespace CoreDomain.Events
{
    public sealed class MeasurementCreated : DomainEvent
    {
        public MeasurementCreated(MeasurementId id,string type,string unit,DateTime eventDateTime) : base(eventDateTime)
        {
            Id = id;
            Type = type;
            Unit = unit;
        }

        public MeasurementId Id { get; }
        public string Type { get; }
        public string Unit { get; }
    }
}
