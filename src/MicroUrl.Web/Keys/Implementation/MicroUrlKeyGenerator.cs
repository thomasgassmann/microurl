namespace MicroUrl.Web.Keys.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Stores;

    public class MicroUrlKeyGenerator : IMicroUrlKeyGenerator
    {
        private const string Characters = "abcdefghiklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new Random();

        private readonly IRedirectableStore _redirectableStore;
        private readonly IKeyValidationService _keyValidationService;

        public MicroUrlKeyGenerator(IRedirectableStore redirectableStore, IKeyValidationService keyValidationService)
        {
            _redirectableStore = redirectableStore;
            _keyValidationService = keyValidationService;
        }

        public async Task<string> GenerateKeyAsync(string customKey = null)
        {
            if (customKey != null)
            {
                return await _redirectableStore.ExistsAsync(customKey)
                    ? throw new KeyGenerationException($"Key already exists {customKey}.")
                    : customKey;
            }

            for (var i = 1; ; i++)
            {
                var key = GenerateKeyOfLength(i);
                if (_keyValidationService.IsKeyValid(key) && !(await _redirectableStore.ExistsAsync(key)))
                {
                    return key;
                }
            }
        }

        private string GenerateKeyOfLength(int length)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append(Characters[_random.Next(0, Characters.Length - 1)]);
            }

            return stringBuilder.ToString();
        }
    }
}