namespace MicroUrl.Storage
{
    using System.Threading.Tasks;

    public interface IEntityStorageService<T> where T : new()
    {
        Task<string> CreateAsync(T entity);

        Task<T> LoadAsync(string key);

        Task<bool> ExistsAsync(string key);
    }
}