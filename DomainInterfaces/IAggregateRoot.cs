using System.Collections.Generic;

namespace DomainInterfaces
{
    public interface IAggregateRoot
    {
        IList<IEvent> Changes { get; }
    }
}
