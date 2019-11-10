namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IMicroUrlStore
    {
        Task<string> CreateAsync(MicroUrl microUrl);
        
        Task<MicroUrl> LoadAsync(string key);
    }
}