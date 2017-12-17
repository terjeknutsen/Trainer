using DomainInterfaces;
using System.Threading.Tasks;

namespace PushUp.Application.Views
{
    interface IUpdateView<TEvent>
    {
        Task Update(IAggregateIdentity id, TEvent @event);
    }
}
