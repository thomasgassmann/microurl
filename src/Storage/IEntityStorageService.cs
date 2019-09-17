namespace MicroUrl.Storage
{
    using System.Threading.Tasks;

    public interface IEntityStorageService<T, TKey> where T : new()
    {
        Task<TKey> CreateAsync(T entity);

        Task<T> LoadAsync(TKey key);

        Task<bool> ExistsAsync(TKey key);
    }
}