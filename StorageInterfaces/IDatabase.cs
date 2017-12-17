using System;

namespace StorageInterfaces
{
    public interface IDatabase
    {
        IDatabaseConnection Connection { get; }
    }
}
