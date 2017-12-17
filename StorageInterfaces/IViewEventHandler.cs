using DomainInterfaces;
using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IViewEventHandler
    {
        bool CanHandle(IEvent @event);
        Task Update(IAggregateIdentity id,IEvent @event);
    }
}
