namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using MicroUrl.Common;
    using MicroUrl.Storage.Abstractions.Filters;
    using MicroUrl.Storage.Abstractions.Shared;

    public class CloudDatastoreStorage<T> : IStorage<T> where T : class, new()
    {
        private readonly IEnvConfigurationStore _configurationStore;
        private readonly IEntityAnalyzer _entityAnalyzer;
        private readonly IKeyFactory _keyFactory;
        private readonly CloudDatastoreEntitySerializer<T> _serializer;

        public CloudDatastoreStorage(
            IEnvConfigurationStore configurationStore,
            IEntityAnalyzer entityAnalyzer,
            IKeyFactory keyFactory)
        {
            _configurationStore = configurationStore;
            _entityAnalyzer = entityAnalyzer;
            _serializer = new CloudDatastoreEntitySerializer<T>(entityAnalyzer);
            _keyFactory = keyFactory;
        }

        public Task<IKey> CreateAsync(T entity) =>
            SaveAsync(entity, true);

        public Task<IKey> UpdateAsync(T entity) =>
            SaveAsync(entity, false);

        public async Task<T> LoadAsync(IKey key)
        {
            var resultEntity = await LookupEntityAsync(key);
            if (resultEntity == null)
            {
                return null;
            }

            var result = new T();
            _serializer.Deserialize(resultEntity, result);
            return result;
        }

        public async IAsyncEnumerable<T> QueryAsync(StorageFilter filter)
        {
            var store = CreateStorage();
            var asyncEnumerable = store.RunQueryLazilyAsync(new Query
            {
                Kind =
                {
                    new KindExpression(_entityAnalyzer.GetEntityName<T>())
                },
                Filter = ConvertFilter(filter)
            });

            var asyncEnumerator = asyncEnumerable.GetEnumerator();
            while (await asyncEnumerator.MoveNext(CancellationToken.None))
            {
                var t = new T();
                _serializer.Deserialize(asyncEnumerator.Current, t);
                yield return t;
            }
        }

        public async Task<bool> ExistsAsync(IKey key)
        {
            var resultEntity = await LookupEntityAsync(key);
            return resultEntity != null;
        }

        private Filter ConvertFilter(StorageFilter filter)
        {
            switch (filter)
            {
                case AndFilter and:
                    return Filter.And(and.Filters.Select(ConvertFilter));
                case ComparisonFilter<T> comparisonFilter:
                    var serializationInfo = _entityAnalyzer.GetSerializationInfo(comparisonFilter.Property);
                    var compareValue = _serializer.GetValueFromPropertyValue(serializationInfo.PropertyType, comparisonFilter.Value);
                    return comparisonFilter.ComparisonType switch
                    {
                        ComparisonType.Equals => Filter.Equal(serializationInfo.Property, compareValue),
                        ComparisonType.GreaterThan => Filter.GreaterThan(serializationInfo.Property, compareValue),
                        ComparisonType.LessThan => Filter.LessThan(serializationInfo.Property, compareValue),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<IKey> SaveAsync(T entity, bool isNew)
        {
            var (storage, keyFactory) = GetStorageAndKeyFactory();

            var keyValue = _entityAnalyzer.GetKeyValue(entity);
            var key = GetKey(keyValue, keyFactory);

            var newEntity = new Entity { Key = key };
            _serializer.Serialize(entity, newEntity);
            if (isNew)
            {
                var newKey = await storage.InsertAsync(newEntity);
                switch (keyValue.KeyType)
                {
                    case KeyType.AutoId:
                        var first = newKey.Path.First();
                        return _keyFactory.CreateFromId(first.Id);
                    case KeyType.StringId:
                        return _keyFactory.CreateFromString(keyValue.StringValue);
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
                        : keyFactory.CreateKey(key.LongValue);
                default:
                    throw new InvalidOperationException("No key type specified");
            }
        }

        private DatastoreDb CreateStorage() =>
            DatastoreDb.Create(_configurationStore.GetStorageSettings().Project);
    }
}
