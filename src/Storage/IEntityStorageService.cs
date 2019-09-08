namespace MicroUrl.Storage
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Entities;

    public interface IEntityStorageService<T> where T : MicroUrlBaseEntity, new()
    {
        Task<string> CreateAsync(T url);

        Task<T> LoadAsync(string key);

        Task<bool> ExistsAsync(string key);
    }
}