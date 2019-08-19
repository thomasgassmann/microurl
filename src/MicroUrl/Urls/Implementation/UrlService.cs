namespace MicroUrl.Urls.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;

    public class UrlService : IUrlService
    {
        private const string Characters = "abcdefghiklmnopqrstuvwxyz0123456789";
        
        private readonly IUrlStorageService _storageService;
        private readonly Random _random = new Random();

        public UrlService(IUrlStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string> SaveAsync(string url)
        {
            var key = await _storageService.SaveAsync(new MicroUrlEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Enabled = true,
                Url = url,
                Key = await GenerateKey()
            });
            return key;
        }

        private async Task<string> GenerateKey()
        {
            for (var i = 1;; i++)
            {
                var key = GenerateKeyOfLength(i++);
                if (!await _storageService.ExistsAsync(key))
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