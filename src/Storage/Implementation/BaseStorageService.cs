namespace MicroUrl.Storage.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;

    public abstract class BaseStorageService<T, TKey> : IEntityStorageService<T, TKey> where T : new()
    {
        private readonly IStorageFactory _storageFactory;

        protected BaseStorageService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }
        
        protected abstract string StorageKey { get; }

        protected abstract Key GetNewKey(KeyFactory keyFactory, T entity);

        protected abstract TKey GetKey(Key key);

        protected abstract bool LogicalExists(Entity entity);

        protected abstract void MapToProperties(T entity, MapField<string, Value> properties);

        protected abstract void MapToEntity(MapField<string, Value> properties, T entity, Key key);
        
        public async Task<TKey> CreateAsync(T entity)
        {
            var dataStore = _storageFactory.GetStorage();
            var keyFactory = dataStore.CreateKeyFactory(StorageKey);
            var key = GetNewKey(keyFactory, entity);
            var newEntity = new Entity { Key = key };
            
            MapToProperties(entity, newEntity.Properties);
            
            await dataStore.InsertAsync(newEntity);

            return GetKey(key);
        }

        public async Task<T> LoadAsync(TKey key)
        {
            var dataStore = _storageFactory.GetStorage();
            var elementKey = GetKeyFromObject(key);
            var result = await dataStore.LookupAsync(elementKey);
            return !LogicalExists(result) ? default : CreateEntity(result.Properties, elementKey);
        }

        public async Task<bool> ExistsAsync(TKey key)
        {
            var dataStore = _storageFactory.GetStorage();
            var result = await dataStore.LookupAsync(GetKeyFromObject(key));
            return LogicalExists(result);
        }

        protected async IAsyncEnumerable<T> ExecuteQueryAsync(Query query)
        {
            var lazyResult = _storageFactory.GetStorage().RunQueryLazilyAsync(query);
            var asyncEnumerator = lazyResult.GetEnumerator();
            while (await asyncEnumerator.MoveNext(CancellationToken.None))
            {
                yield return CreateEntity(asyncEnumerator.Current.Properties, asyncEnumerator.Current.Key);
            }
        }

        protected T CreateEntity(MapField<string, Value> properties, Key key)
        {            
            var instance = new T();
            MapToEntity(properties, instance, key);
            return instance;
        }

        private Key GetKeyFromObject(TKey key)
        {
            if (key.GetType() == typeof(long))
            {
                return new Key().WithElement(StorageKey, long.Parse(key.ToString()));
            }
            
            if (key.GetType() == typeof(string))
            {
                return new Key().WithElement(StorageKey, key.ToString());
            }
            
            throw new ArgumentException(nameof(key));
        }
    }
}