using System;
using System.Collections.Generic;
using ViewModels;
using StorageInterfaces;
using System.Threading.Tasks;
using System.Linq;

namespace PushupAndroid.TestRepository
{
    public sealed class ChallengeRepository : IRepository<ChallengeViewModel>
    {
        public event EventHandler OnChange;

        public Task AddAsync(ChallengeViewModel model)
        {
            TestData.ChallengeViewModels.Add(model);
            OnChange?.Invoke(this,EventArgs.Empty);
            return Task.CompletedTask;
        }

        public async Task AddAsync(IEnumerable<ChallengeViewModel> models)
        {
            foreach (var challenge in models)
                await AddAsync(challenge);
        }

        public Task<IEnumerable<ChallengeViewModel>> AllAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<ChallengeViewModel>>();
            tcs.SetResult(TestData.ChallengeViewModels);
            return tcs.Task;
        }

        public Task<ChallengeViewModel> GetAsync(Guid key)
        {
            var tcs = new TaskCompletionSource<ChallengeViewModel>();
            tcs.SetResult(TestData.ChallengeViewModels.First(m => m.Id == key));
            return tcs.Task;
        }

        public Task RemoveAsync(ChallengeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ChallengeViewModel model)
        {
            var modelToUpdate = TestData.ChallengeViewModels.First(m => m.Id == model.Id);
            modelToUpdate = model;
            OnChange?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }
    }
}