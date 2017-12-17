using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using StorageInterfaces;
using System.Threading.Tasks;

namespace PushupAndroid.TestRepository
{
    public sealed class WorkoutRepository : IRepository<WorkoutViewModel>
    {
        public event EventHandler OnChange;

        public Task AddAsync(WorkoutViewModel model)
        {
            TestData.WorkoutViewModels.Add(model);
            OnChange?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }

        public async Task AddAsync(IEnumerable<WorkoutViewModel> models)
        {
            foreach (var workout in models)
                await AddAsync(workout);
        }

        public Task<IEnumerable<WorkoutViewModel>> AllAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<WorkoutViewModel>>();
            tcs.SetResult(TestData.WorkoutViewModels.Where(w => !w.Executed));
            return tcs.Task;
        }

        public Task<WorkoutViewModel> GetAsync(Guid key)
        {
            var tcs = new TaskCompletionSource<WorkoutViewModel>();
            tcs.SetResult(TestData.WorkoutViewModels.First(m => m.Id == key));
            return tcs.Task;
        }

        public Task RemoveAsync(WorkoutViewModel model)
        {
            OnChange?.Invoke(this, EventArgs.Empty);
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkoutViewModel model)
        {
            WorkoutViewModel workoutToReplace;
            foreach (var workout in TestData.WorkoutViewModels)
            {
                if (workout.Id == model.Id)
                {
                    workoutToReplace = workout;
                    break;
                }
            }
            workoutToReplace = model;
            OnChange?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }
    }
}