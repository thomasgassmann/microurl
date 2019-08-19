namespace MicroUrl.Urls.Implementation
{
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

        public async Task SaveAsync(MicroUrlEntity url)
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
        }

        public async Task<MicroUrlEntity> LoadAsync(string key)
        {
            var result = await _datastore.LookupAsync(new Key().WithElement(Kind, key));
            if (result == null)
            {
                return null;
            }

            return new MicroUrlEntity
            {
                Created = result["created"].TimestampValue,
                Enabled = result["enabled"].BooleanValue,
                Key = result["key"].StringValue,
                Url = result["url"].StringValue
            };
        }
    }
}