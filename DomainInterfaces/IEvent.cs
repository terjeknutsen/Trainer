using System;

namespace DomainInterfaces
{
    public interface IEvent
    {
        DateTime OccurredOn { get; }
    }
}
