using Domain.Core.Base;
using System.Collections.Generic;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;
using System;

namespace CoreDomain.States
{
    public sealed class MeasurementState : State
    {
        public MeasurementState(IEnumerable<IEvent> events) : base(events)
        {}
        public string Type { get; set; }
        public string Unit { get; set; }
        public MeasurementId Id { get; set; }
        public Dictionary<DateTime, double> Values { get; } = new Dictionary<DateTime, double>();

        public void When(MeasurementCreated @event)
        {
            Created = true;
            Type = @event.Type;
            Id = @event.Id;
            Unit = @event.Unit;
        }
        public void When(MeasurementUnitChanged @event)
        {
            Unit = @event.Unit;
        }
        public void When(MeasurementTypeChanged @event)
        {
            Type = @event.Type;
        }
        public void When(MeasurementValueAdded @event)
        {
            Values.Add(@event.OccurredOn, @event.Value);
        }
    }
}
