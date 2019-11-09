namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IRedirectableStore
    {
        Task<Redirectable> LoadAsync(string key);
    }
}