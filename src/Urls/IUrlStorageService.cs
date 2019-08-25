namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IUrlStorageService
    {
        Task<string> SaveAsync(MicroUrlEntity url);

        Task<MicroUrlEntity> LoadAsync(string key);

        Task<bool> ExistsAsync(string key);
    }
}