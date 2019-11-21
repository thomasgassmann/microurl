namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IUserStore
    {
        Task CreateAsync(User user);

        Task<User> LoadAsync(string userName);

        Task<bool> ExistsAsync(string username);
    }
}
