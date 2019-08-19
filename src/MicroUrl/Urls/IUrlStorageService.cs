namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IUrlStorageService
    {
        Task<long> SaveAsync(MicroUrlEntity url);

        Task<MicroUrlEntity> LoadAsync(long id);
    }
}