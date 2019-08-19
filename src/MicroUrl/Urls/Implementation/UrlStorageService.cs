namespace MicroUrl.Urls.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;

    public class UrlStorageService : IUrlStorageService
    {
        private readonly DatastoreDb _datastore;
        
        private const string Kind = "MicroUrl";

        public UrlStorageService(IOptions<MicroUrlSettings> options)
        {
            _datastore = DatastoreDb.Create(options.Value.Storage.Project);
        }

        public async Task<string> SaveAsync(MicroUrlEntity url)
        {
            var key = _datastore.CreateKeyFactory(Kind).CreateKey(url.Key);
            await _datastore.InsertAsync(new Entity
            {
                Key = key,
                Properties =
                {
                    { "url", url.Url },
                    { "enabled", url.Enabled },
                    { "created", url.Created }
                }
            });
            return key.Path.First().Name;
        }

        public async Task<MicroUrlEntity> LoadAsync(string key)
        {
            var result = await _datastore.LookupAsync(GetKey(key));
            if (result == null)
            {
                return null;
            }

            return new MicroUrlEntity
            {
                Created = result["created"].TimestampValue,
                Enabled = result["enabled"].BooleanValue,
                Key = key,
                Url = result["url"].StringValue
            };
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var result = await _datastore.LookupAsync(GetKey(key));
            return result != null;
        }

        private static Key GetKey(string key) => new Key().WithElement(Kind, key);
    }
}