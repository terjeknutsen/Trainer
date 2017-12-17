using System;

namespace DomainInterfaces
{
    public interface IAggregateIdentity
    {
        Guid Guid { get; }
    }
}
