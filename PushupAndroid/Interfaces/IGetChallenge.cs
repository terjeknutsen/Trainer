using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace no.trainer.personal.Interfaces
{
    internal interface IGetChallenge
    {
        Task<IEnumerable<ChallengeViewModel>> Challenges { get; }
    }
}