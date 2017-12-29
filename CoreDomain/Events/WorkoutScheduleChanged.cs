using CoreDomain.Base;
using System;
using System.Collections.Generic;

namespace CoreDomain.Events
{
    public sealed class WorkoutScheduleChanged : DomainEvent
    {
        public WorkoutScheduleChanged(List<KeyValuePair<TimeSpan,int>> schedule, DateTime eventDateTime):base(eventDateTime)
        {
            Schedule = schedule;
        }
        public IEnumerable<KeyValuePair<TimeSpan,int>> Schedule { get; }
    } 
}
