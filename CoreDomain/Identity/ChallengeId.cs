using CoreDomain.Base;
using System;

namespace Domain.Identity
{
    public sealed class ChallengeId : AggregateId, IEquatable<ChallengeId>
    {
        public ChallengeId(Guid guid):base(guid)
        {}

        public bool Equals(ChallengeId other)
        {
            if (other == null) return false;
            return ToString().Equals(other.ToString());
            
        }
        public override bool Equals(object obj)
        {
            var id = obj is ChallengeId ? (ChallengeId)obj : null;
            return Equals(id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(ChallengeId)}_{Guid}";
        }
    }
}
