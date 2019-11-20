namespace MicroUrl.Web.Authentication
{
    using System.Threading.Tasks;

    public interface IUserManager
    {
        Task SignUpAsync(string username, string password);

        Task<bool> Verify(string username, string password);
    }
}
