namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IUrlStorageService
    {
        Task SaveAsync(MicroUrlEntity url);

        Task<MicroUrlEntity> LoadAsync(string key);
    }
}