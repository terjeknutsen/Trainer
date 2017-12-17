using CoreDomain.Base;
using System;

namespace Domain.Identity
{
    public sealed class WorkoutId : AggregateId, IEquatable<WorkoutId>
    {
        public WorkoutId(Guid guid): base(guid)
        {}

        public bool Equals(WorkoutId other)
        {
            if (other == null) return false;
            return ToString().Equals(other.ToString());
        }
        public override bool Equals(object obj)
        {
            var id = obj is WorkoutId ? (WorkoutId)obj : null;
            return Equals(id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(WorkoutId)}_{Guid}";
        }
    }
}
