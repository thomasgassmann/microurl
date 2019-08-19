namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IUrlStorageService
    {
        Task Save(MicroUrlEntity url);

        Task<MicroUrlEntity> Load(string key);
    }
}