using System.Threading.Tasks;

namespace StorageInterfaces
{
    public interface ICreate<T>
    {
        Task CreateAsync(T model);
    }
}
