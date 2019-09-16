namespace MicroUrl.Storage.Implementation
{
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;

    public abstract class BaseStorageService<T> : IEntityStorageService<T> where T : new()
    {
        private readonly IStorageFactory _storageFactory;

        protected BaseStorageService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }
        
        protected abstract string StorageKey { get; }

        protected abstract Key GetNewKey(KeyFactory keyFactory, T entity);

        protected abstract string GetKeyString(Key key);

        protected abstract bool LogicalExists(Entity entity);

        protected abstract void MapToProperties(T entity, MapField<string, Value> properties);

        protected abstract void MapToEntity(MapField<string, Value> properties, T entity);
        
        public async Task<string> CreateAsync(T entity)
        {
            var dataStore = _storageFactory.GetStorage();
            var keyFactory = dataStore.CreateKeyFactory(StorageKey);
            var key = GetNewKey(keyFactory, entity);
            var newEntity = new Entity { Key = key };
            
            MapToProperties(entity, newEntity.Properties);

            await dataStore.InsertAsync(newEntity);
            return GetKeyString(key);
        }

        public async Task<T> LoadAsync(string key)
        {
            var dataStore = _storageFactory.GetStorage();
            var elementKey = new Key().WithElement(StorageKey, key);
            var result = await dataStore.LookupAsync(elementKey);
            if (!LogicalExists(result))
            {
                return default(T);
            }

            var instance = new T();

            MapToEntity(result.Properties, instance);

            return instance;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var dataStore = _storageFactory.GetStorage();
            var elementKey = new Key().WithElement(StorageKey, key);
            var result = await dataStore.LookupAsync(elementKey);
            return LogicalExists(result);
        }
    }
}