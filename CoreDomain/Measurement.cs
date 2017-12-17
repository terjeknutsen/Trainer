using Domain.Core.Base;
using DomainInterfaces;
using System;
using System.Collections.Generic;
using CoreDomain.Identity;
using CoreDomain.States;
using CoreDomain.Events;

namespace CoreDomain
{
    public sealed class Measurement : AggregateRoot
    {
        readonly MeasurementState state;
        public Measurement(IEnumerable<IEvent> events)
        {
            State = new MeasurementState(events);
            state = State as MeasurementState;
        }

        public void Create(MeasurementId measurementId, string type,string unit)
        {
            if (State.Created)
                throw new InvalidOperationException($"{nameof(Measurement)}-{measurementId} already created");
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("Measurement type cannot be null");
            if (string.IsNullOrEmpty(unit))
                throw new ArgumentNullException("Measurement unit cannot be null");
            Apply(new MeasurementCreated(measurementId, type,unit, DateTime.Now));
        }

        public void AddValue(double value, DateTime eventDateTime)
        {
            Apply(new MeasurementValueAdded(value, eventDateTime));
        }

        public void ChangeUnit(string unit,DateTime eventDateTime)
        {
            if (state.Unit == unit) return;
            Apply(new MeasurementUnitChanged(unit, eventDateTime));
        }
        public void ChangeType(string type, DateTime eventDateTime)
        {
            if (state.Type == type) return;
            Apply(new MeasurementTypeChanged(type, eventDateTime));
        }
        
    }
}
