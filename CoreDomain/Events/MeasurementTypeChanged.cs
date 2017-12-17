using CoreDomain.Base;
using System;

namespace CoreDomain.Events
{
    public sealed class MeasurementTypeChanged : DomainEvent
    {
        public MeasurementTypeChanged(string type, DateTime eventDateTime) : base(eventDateTime)
        {
            Type = type;
        }

        public string Type { get; }
    }
}
