using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface IDelete<T>
    {
        Task DeleteAsync(T model);
    }
}
