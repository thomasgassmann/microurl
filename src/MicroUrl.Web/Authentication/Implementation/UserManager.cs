namespace MicroUrl.Web.Authentication.Implementation
{
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public class UserManager : IUserManager
    {
        private const int Iterations = 100000;
        private const int SaltSize = 128;
        private const int HashSize = 128;

        private readonly IUserStore _userStore;

        public UserManager(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public Task SignUpAsync(string username, string password)
        {
            var userPassword = DeriveNewPassword(password);
            return _userStore.CreateAsync(new User
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
            var salt = new byte[SaltSize];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hashedPassword = pbkdf2.GetBytes(HashSize);
            return new UserPassword
            {
                Hash = hashedPassword,
                Iterations = Iterations,
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
