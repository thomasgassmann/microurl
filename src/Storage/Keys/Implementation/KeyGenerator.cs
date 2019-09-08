namespace MicroUrl.Urls.Implementation
{
    using System.Text;
    using System.Threading.Tasks;
    using MicroUrl.Storage;

    public class KeyGenerator : IKeyGenerator
    {
        private readonly IStorageFactory _storageFactory;
        
        public Task<string> GetKeyAsync(string customKey = null)
        {
            throw new System.NotImplementedException();
        }
        
        private async Task<string> GenerateKey()
        {
            for (var i = 1;; i++)
            {
                var key = GenerateKeyOfLength(i);
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