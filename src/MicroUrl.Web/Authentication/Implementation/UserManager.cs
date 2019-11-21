namespace MicroUrl.Web.Authentication.Implementation
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;

    public class UserManager : IUserManager
    {
        private const int DefaultIterations = 100000;
        private const int DefaultSaltSize = 128;
        private const int DefaultHashSize = 128;

        private readonly IUserStore _userStore;

        public UserManager(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task SignUpAsync(string username, string password)
        {
            if (await _userStore.ExistsAsync(username))
            {
                throw new UserAlreadyExistsException(username);
            }

            var userPassword = DeriveNewPassword(password);
            await _userStore.CreateAsync(new User
            {
                Created = DateTime.Now,
                Password = userPassword,
                UserName = username
            });
        }

        public async Task<bool> Verify(string username, string password)
        {
            var user = await _userStore.LoadAsync(username);
            return VerifyPassword(user.Password, password);
        }

        private UserPassword DeriveNewPassword(string password)
        {
            var salt = new byte[DefaultSaltSize];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DefaultIterations);
            var hashedPassword = pbkdf2.GetBytes(DefaultHashSize);
            return new UserPassword
            {
                Hash = hashedPassword,
                Iterations = DefaultIterations,
                Salt = salt
            };
        }

        private bool VerifyPassword(UserPassword userPassword, string password)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, userPassword.Salt, userPassword.Iterations);
            var result = pbkdf2.GetBytes(userPassword.Hash.Length);
            return userPassword.Hash.AsSpan().SequenceEqual(result.AsSpan());
        }
    }
}
