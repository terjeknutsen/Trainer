using CoreDomain.Base;
using System;
using System.Collections.Generic;

namespace CoreDomain.Events
{
    public sealed class RestingCycleChanged : DomainEvent
    {
        public RestingCycleChanged(IDictionary<int,bool> restingCycle, DateTime eventDateTime): base(eventDateTime)
        {
            RestingCycle = restingCycle;
        }

        public IDictionary<int,bool> RestingCycle { get; }
    }
}
