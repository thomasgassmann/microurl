namespace DefaultNamespace
{
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Storage;

    public const string Storage = "Links";
    
    public class CloudDatastoreStorage : IUrlKeyValueStorage
    {
        private readonly DatastoreDb _datastore;

        public CloudDatastoreStorage(IOptions<UrlSettings> options)
        {
            _datastore = DatastoreDb.Create(options.Value.Storage.Project);
        }

        public Task SaveAsync<T>(string key, T url)
        {
            var key = _datastore.CreateKeyFactory(GetKind<T>()).CreateKey(key);
            var entity = new Entity();
            foreach (var property in typeof(T).GetProperties())
            {
                entity[property.Name] = property.GetValue(url);
            }
            
            _datastore.UpsertAsync(new Entity
            {

            });
        }

        public Task<T> LoadAsync<T>(string key)
        {
            return _datastore.LookupAsync(key)
        }

        public Task<bool> Exists<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        private Key GetInternalKey<T>(string key) => _datastore.CreateKeyFactory(GetKind<T>()).CreateKey(key);

        private string GetKind<T>() => typeof(T).Name;
    }
}