namespace MicroUrl.Urls.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Entities;
    using MicroUrl.Storage.Implementation;

    public class MicroUrlKeyGenerator : IMicroUrlKeyGenerator
    {
        private const string Characters = "abcdefghiklmnopqrstuvwxyz0123456789";
        
        private readonly Random _random = new Random();
        private readonly IStorageFactory _storageFactory;

        public MicroUrlKeyGenerator(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
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
            var storage = _storageFactory.GetStorage();
            var entity = await storage.LookupAsync(new Key().WithElement(
                UrlBaseStorageService<MicroUrlBaseEntity>.MicroUrlStorageKey,
                key));
            return entity != null;
        }
    }
}