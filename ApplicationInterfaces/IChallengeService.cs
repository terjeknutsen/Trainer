using System.Threading.Tasks;

namespace ApplicationInterfaces
{
    public interface IApplicationService
    {
        Task ExecuteCommand(ICommand cmd);
    }
}
