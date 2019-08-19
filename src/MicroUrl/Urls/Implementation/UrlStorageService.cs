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

        public async Task<long> SaveAsync(MicroUrlEntity url)
        {
            var key = _datastore.CreateKeyFactory(Kind).CreateIncompleteKey();
            var createdKey = await _datastore.InsertAsync(new Entity
            {
                Key = key,
                Properties =
                {
                    { "url", url.Url },
                    { "enabled", url.Enabled },
                    { "created", url.Created }
                }
            });
            return createdKey.Path.First().Id;
        }

        public async Task<MicroUrlEntity> LoadAsync(long id)
        {
            var result = await _datastore.LookupAsync(new Key().WithElement(Kind, id));
            if (result == null)
            {
                return null;
            }

            return new MicroUrlEntity
            {
                Created = result["created"].TimestampValue,
                Enabled = result["enabled"].BooleanValue,
                Id = result["key"].IntegerValue,
                Url = result["url"].StringValue
            };
        }
    }
}