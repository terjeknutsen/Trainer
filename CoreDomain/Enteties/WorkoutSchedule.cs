using System;
using System.Collections.Generic;

namespace CoreDomain.Enteties
{
    public sealed class WorkoutSchedule
    {
        public List<KeyValuePair<TimeSpan, int>> Schedule { get; set; }
    }
}
