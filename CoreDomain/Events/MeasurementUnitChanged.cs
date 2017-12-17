using CoreDomain.Base;
using System;

namespace CoreDomain.Events
{
    public sealed class MeasurementUnitChanged : DomainEvent
    {
        public MeasurementUnitChanged(string unit,DateTime eventDateTime) : base(eventDateTime)
        {
            Unit = unit;
        }

        public string Unit { get; }
    }
}
