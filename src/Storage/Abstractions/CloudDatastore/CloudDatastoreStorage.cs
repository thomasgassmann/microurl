namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using Google.Cloud.Datastore.V1;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Storage.Abstractions.Shared;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CloudDatastoreStorage<T> : IStorage<T> where T : class, new()
    {
        private readonly IOptions<MicroUrlSettings> _options;
        private readonly IEntityAnalyzer _entityAnalyzer;
        private readonly IKeyFactory _keyFactory;

        public CloudDatastoreStorage(
            IOptions<MicroUrlSettings> options,
            IEntityAnalyzer entityAnalyzer,
            IKeyFactory keyFactory)
        {
            _options = options;
            _entityAnalyzer = entityAnalyzer;
        }

        public async Task<T> LoadAsync(IKey key)
        {
            var (storage, keyFactory) = GetStorageAndKeyFactory();
            var dataStoreKey = GetKey(key, keyFactory);
            var results = await storage.LookupAsync(dataStoreKey);

            
        }

        public async Task<IKey> SaveAsync(T entity)
        {
            var (storage, keyFactory) = GetStorageAndKeyFactory();
            
            var keyValue = _entityAnalyzer.GetKeyValue(entity);
            var isNew = keyValue.LongValue == null && keyValue.StringValue == null;
            var key = GetKey(keyValue, keyFactory);

            var newEntity = new Entity { Key = key };
            if (isNew)
            {
                var newKey = await storage.InsertAsync(newEntity);
                var first = newKey.Path.First();
                switch (first.IdTypeCase)
                {
                    case Key.Types.PathElement.IdTypeOneofCase.Id:
                        return _keyFactory.CreateFromId(first.Id);
                    case Key.Types.PathElement.IdTypeOneofCase.Name:
                        return _keyFactory.CreateFromString(first.Name);
                    default:
                        throw new ArgumentException();
                }
            }
            
            await storage.UpdateAsync(newEntity);
            return keyValue;
        }

        private (DatastoreDb, KeyFactory) GetStorageAndKeyFactory()
        {
            var entityName = _entityAnalyzer.GetEntityName<T>();

            var storage = CreateStorage();
            var keyFactory = storage.CreateKeyFactory(entityName);

            return (storage, keyFactory);
        }

        private Key GetKey(IKey key, KeyFactory keyFactory)
        {
            switch (key.KeyType)
            {
                case KeyType.StringId:
                    if (key.StringValue == null)
                    {
                        throw new InvalidOperationException("stringified key cannot be null");
                    }

                    return keyFactory.CreateKey(key.StringValue);
                case KeyType.AutoId:
                    return !key.LongValue.HasValue 
                        ? keyFactory.CreateIncompleteKey()
                        : keyFactory.CreateKey(key.LongValue.Value);
                default:
                    throw new InvalidOperationException("No key type specified");
            }
        }

        private DatastoreDb CreateStorage() =>
            DatastoreDb.Create(_options.Value.Storage.Project);
    }
}
