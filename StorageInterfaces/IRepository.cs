using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T model);
        Task AddAsync(IEnumerable<T> models);
        Task RemoveAsync(T model);
        Task UpdateAsync(T model);
        Task<IEnumerable<T>> AllAsync();
        Task<T> GetAsync(Guid key);
        event EventHandler OnChange;
    }
}
