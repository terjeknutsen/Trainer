using CoreDomain.Base;
using System;

namespace CoreDomain.Identity
{
    public sealed class MeasurementId : AggregateId , IEquatable<MeasurementId>
    {
        public MeasurementId(Guid guid) : base(guid)
        {}

        public bool Equals(MeasurementId other)
        {
            if (other == null) return false;
            return ToString().Equals(other.ToString());
        }
        public override bool Equals(object obj)
        {
            var id = obj is MeasurementId ? (MeasurementId)obj : null;
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(MeasurementId)}_{Guid}";
        }
    }
}
