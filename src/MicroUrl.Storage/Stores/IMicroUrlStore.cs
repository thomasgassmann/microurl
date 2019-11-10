namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IMicroUrlStore
    {
        Task<string> SaveAsync(MicroUrl microUrl);
        
        Task<MicroUrl> LoadAsync(string key);
    }
}