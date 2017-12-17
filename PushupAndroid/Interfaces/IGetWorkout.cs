using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace no.trainer.personal.Interfaces
{
    internal interface IGetWorkout
    {
        Task<IEnumerable<WorkoutViewModel>> Workouts { get; }
    }
}