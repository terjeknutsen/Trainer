using System.Threading.Tasks;
using StorageInterfaces;
using ViewModels;

namespace PushupAndroid.TestRepository
{
    public sealed class ChallengeCreator : ICreate<ChallengeViewModel>
    {
        public Task CreateAsync(ChallengeViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}