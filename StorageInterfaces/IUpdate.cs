using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IUpdate<T>
    {
        Task UpdateAsync(T model);
    }
}
