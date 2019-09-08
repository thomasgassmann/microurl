namespace MicroUrl.Storage.Implementation
{
    using Google.Cloud.Datastore.V1;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;

    public class StorageFactory : IStorageFactory
    {
        private readonly IOptions<MicroUrlSettings> _options;
        private DatastoreDb _cachedDataStore;
        
        public StorageFactory(IOptions<MicroUrlSettings> options)
        {
            _options = options;
        }
        
        public DatastoreDb GetStorage()
        {
            return _cachedDataStore ??= DatastoreDb.Create(_options.Value.Storage.Project);
        }
    }
}