using System;
using System.Collections.Generic;
using ViewModels;
using StorageInterfaces;
using System.Threading.Tasks;

namespace PushupAndroid.TestRepository
{
    public sealed class MeasurementRepository : IRepository<MeasurementViewModel>
    {
        public event EventHandler OnChange;

        public Task AddAsync(MeasurementViewModel model)
        {
            OnChange?.Invoke(this, EventArgs.Empty);
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<MeasurementViewModel> models)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeasurementViewModel>> AllAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<MeasurementViewModel>>();
            tcs.SetResult(TestData.MeasurementViewModels);
            return tcs.Task;
        }

        public Task<MeasurementViewModel> GetAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(MeasurementViewModel model)
        {
            OnChange?.Invoke(this, EventArgs.Empty);
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MeasurementViewModel model)
        {
            OnChange?.Invoke(this, EventArgs.Empty);
            throw new NotImplementedException();
        }
    }
}