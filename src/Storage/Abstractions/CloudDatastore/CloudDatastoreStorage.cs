namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using Google.Cloud.Datastore.V1;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Storage.Abstractions.Shared;
    using System;
    using System.Threading.Tasks;

    public class CloudDatastoreStorage<T> : IStorage<T> where T : class, new()
    {
        private readonly IOptions<MicroUrlSettings> _options;
        private readonly IEntityAnalyzer _entityAnalyzer;

        public CloudDatastoreStorage(IOptions<MicroUrlSettings> options, IEntityAnalyzer entityAnalyzer)
        {
            _options = options;
            _entityAnalyzer = entityAnalyzer;
        }

        public async Task<T> LoadAsync(IKey key)
        {
            var keyType = _entityAnalyzer.GetKeyType<T>();
            var entityName = _entityAnalyzer.GetEntityName<T>();

            var storage = CreateStorage();
            var keyFactory = storage.CreateKeyFactory(entityName);

            var dataStoreKey = GetKey(keyType, key, keyFactory);
            var results = await storage.LookupAsync(dataStoreKey);

            
        }

        public Task<IKey> SaveAsync(T entity)
        {
            var keyValue = _entityAnalyzer.GetKeyValue(entity);
            keyValue.
        }

        private Key GetKey(KeyType keyType, IKey key, KeyFactory keyFactory)
        {
            switch (keyType)
            {
                case KeyType.StringId:
                    if (key.StringValue == null)
                    {
                        throw new InvalidOperationException("stringified key cannot be null");
                    }

                    return keyFactory.CreateKey(key.StringValue);
                case KeyType.AutoId:
                    return key.IsNew ? keyFactory.CreateIncompleteKey() : keyFactory.CreateKey(key.LongValue.Value);
                default:
                    throw new InvalidOperationException("No key type specified");
            }
        }

        private DatastoreDb CreateStorage() =>
            DatastoreDb.Create(_options.Value.Storage.Project);
    }
}
