using StorageInterfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public sealed class CreateReadUpdateDelete<T> : ICreate<T>, IRead<T>,IUpdate<T>,IDelete<T>
    {
        private readonly IDatabase database;

        public CreateReadUpdateDelete(IDatabase database)
        {
            this.database = database;
        }

        public Task<IEnumerable<T>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(T model)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }
    }
}
