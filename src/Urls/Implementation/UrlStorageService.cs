namespace MicroUrl.Urls.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using MicroUrl.Storage;
    using MicroUrl.Urls.Models;

    public abstract class MicroUrlBaseStorageService
    {
        private readonly IStorageFactory _storageFactory;

        public MicroUrlBaseStorageService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }

        public async Task<string> SaveAsync(MicroUrlBaseEntity entity)
        {
            var datastore = _storageFactory.GetStorage();
            var key = datastore.CreateKeyFactory(Kind).CreateKey(url.Key);
            await datastore.InsertAsync(new Entity
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
            var datastore = _storageFactory.GetStorage();
            var result = await datastore.LookupAsync(GetKey(key));
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
            var datastore = _storageFactory.GetStorage();
            var result = await datastore.LookupAsync(GetKey(key));
            return result != null;
        }

        private static Key GetKey(string key) => new Key().WithElement(Kind, key);
    }
}