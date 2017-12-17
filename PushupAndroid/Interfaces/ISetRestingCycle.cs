using System;
using System.Collections.Generic;

namespace no.trainer.personal.Interfaces
{
    interface ISetRestingCycle
    {
        void SetChallengeRestingCycle(Guid id,Dictionary<int, bool> cycle);
    }
}