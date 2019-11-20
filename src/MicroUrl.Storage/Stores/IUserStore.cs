namespace MicroUrl.Storage.Stores
{
    using MicroUrl.Storage.Dto;
    using System.Threading.Tasks;

    public interface IUserStore
    {
        Task CreateAsync(User user);

        Task<User> LoadAsync(string userName);

        Task<bool> ExistsAsync(string username);
    }
}
