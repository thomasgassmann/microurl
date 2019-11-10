namespace MicroUrl.Storage.Abstractions
{
    using System.Threading.Tasks;

    public interface IStorage<T> where T : class, new()
    {
        Task<IKey> CreateAsync(T entity);

        Task<IKey> UpdateAsync(T entity);

        Task<T> LoadAsync(IKey key);

        Task<bool> ExistsAsync(IKey key);
    }
}