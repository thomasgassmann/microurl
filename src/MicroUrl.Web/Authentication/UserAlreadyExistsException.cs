namespace MicroUrl.Web.Authentication
{
    using MicroUrl.Common;

    public class UserAlreadyExistsException : MicroUrlException
    {
        public UserAlreadyExistsException(string username) : base($"{username} already exists")
        {
        }
    }
}
