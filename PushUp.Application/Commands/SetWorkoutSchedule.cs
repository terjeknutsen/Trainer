using ApplicationInterfaces;
using System;
using System.Collections.Generic;

namespace PushUp.Application.Commands
{
    public sealed class SetWorkoutSchedule : ICommand
    {
        public SetWorkoutSchedule(Guid id,List<KeyValuePair<TimeSpan,int>> schedule)
        {
            Id = id;
            Schedule = schedule;
        }
        public Guid Id { get; }
        public List<KeyValuePair<TimeSpan,int>> Schedule { get; }
    }
}
