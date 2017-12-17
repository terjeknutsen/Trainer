using System.Collections.Generic;
using DomainInterfaces;
using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IViewStore
    {
        Task Update(IAggregateIdentity id, IList<IEvent> changes);
        void TryAddEventHandler(IViewEventHandler eventHandler);
    }
}
