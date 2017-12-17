using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IRead<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> AllAsync();
    }
}
