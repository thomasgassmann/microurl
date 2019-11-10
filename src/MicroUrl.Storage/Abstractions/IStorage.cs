namespace MicroUrl.Storage.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Abstractions.Filters;

    public interface IStorage<T> where T : class, new()
    {
        Task<IKey> CreateAsync(T entity);

        Task<IKey> UpdateAsync(T entity);

        Task<T> LoadAsync(IKey key);

        IAsyncEnumerable<T> QueryAsync(StorageFilter filter);

        Task<bool> ExistsAsync(IKey key);
    }
}