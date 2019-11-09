namespace MicroUrl.Storage.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Entities;

    public class MicroUrlKeyGenerator : IMicroUrlKeyGenerator
    {
        private const string Characters = "abcdefghiklmnopqrstuvwxyz0123456789";
        
        private readonly Random _random = new Random();
        
        private readonly IStorageFactory _storageFactory;
        private readonly IKeyFactory _keyFactory;

        public MicroUrlKeyGenerator(IStorageFactory storageFactory, IKeyFactory keyFactory)
        {
            _storageFactory = storageFactory;
            _keyFactory = keyFactory;
        }
        
        public async Task<string> GenerateKeyAsync(string customKey = null)
        {
            if (customKey != null)
            {
                return await ExistsAsync(customKey)
                    ? throw new KeyGenerationException($"Key already exists {customKey}.")
                    : customKey;
            }
            
            for (var i = 1;; i++)
            {
                var key = GenerateKeyOfLength(i);
                if (!await ExistsAsync(key))
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

        private async Task<bool> ExistsAsync(string key)
        {
            var storage = _storageFactory.CreateStorage<MicroUrlEntity>();
            return await storage.ExistsAsync(_keyFactory.CreateFromString(key));
        }
    }
}