namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IRedirectableStore
    {
        Task<Redirectable> LoadAsync(string key);

        Task<string> CreateAsync(Redirectable redirectable);

        Task<bool> ExistsAsync(string key);
    }
}