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
        private readonly IEntitySerializer<T, Entity> _serializer;

        public CloudDatastoreStorage(
            IOptions<MicroUrlSettings> options,
            IEntityAnalyzer entityAnalyzer,
            IKeyFactory keyFactory)
        {
            _options = options;
            _entityAnalyzer = entityAnalyzer;
            _serializer = new CloudDatastoreEntitySerializer<T>(entityAnalyzer);
            _keyFactory = keyFactory;
        }

        public async Task<T> LoadAsync(IKey key)
        {
            var resultEntity = await LookupEntityAsync(key);

            var result = new T();
            
            _serializer.Deserialize(resultEntity, result);

            return result;
        }

        public async Task<bool> ExistsAsync(IKey key)
        {
            var resultEntity = await LookupEntityAsync(key);
            return resultEntity != null;
        }

        public async Task<IKey> SaveAsync(T entity)
        {
            var (storage, keyFactory) = GetStorageAndKeyFactory();
            
            var keyValue = _entityAnalyzer.GetKeyValue(entity);
            var isNew = keyValue.LongValue == default && keyValue.StringValue == null;
            var key = GetKey(keyValue, keyFactory);

            var newEntity = new Entity { Key = key };
            _serializer.Serialize(entity, newEntity);
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

        private async Task<Entity> LookupEntityAsync(IKey key)
        {
            var (storage, keyFactory) = GetStorageAndKeyFactory();
            var dataStoreKey = GetKey(key, keyFactory);
            var resultEntity = await storage.LookupAsync(dataStoreKey);
            return resultEntity;
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
                    return key.LongValue == default
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
