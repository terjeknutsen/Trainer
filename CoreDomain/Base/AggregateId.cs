using DomainInterfaces;
using System;

namespace CoreDomain.Base
{
    public abstract class AggregateId : IAggregateIdentity
    {
        readonly Guid guid;
        public AggregateId(Guid guid)
        {
            this.guid = guid;
        }

        public Guid Guid => guid;
    }
}
