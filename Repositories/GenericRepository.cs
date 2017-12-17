using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public sealed class GenericRepository<T> : IRepository<T>
    {
        readonly IRead<T> reader;
        readonly IUpdate<T> updater;
        readonly ICreate<T> creator;
        readonly IDelete<T> deleter;

        public GenericRepository(
            IRead<T> reader, 
            IUpdate<T> updater, 
            ICreate<T> creator, 
            IDelete<T> deleter)
        {
            this.reader = reader;
            this.updater = updater;
            this.creator = creator;
            this.deleter = deleter;
        }

        public event EventHandler OnChange;

        public async Task AddAsync(T model)
        {
            await creator.CreateAsync(model);
            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public async Task AddAsync(IEnumerable<T> models)
        {
            foreach(var model in models)
            {
                await creator.CreateAsync(model);
            }
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await reader.AllAsync();
        }

        public async Task<T> GetAsync(Guid key)
        {
            return await reader.GetAsync(key);
        }

        public async Task RemoveAsync(T model)
        {
            await deleter.DeleteAsync(model);
            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public async Task UpdateAsync(T model)
        {
            await updater.UpdateAsync(model);
            OnChange.Invoke(this, EventArgs.Empty);
        }
    }
}
