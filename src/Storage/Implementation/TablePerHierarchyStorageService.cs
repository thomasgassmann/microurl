namespace MicroUrl.Storage.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Entities;

    public abstract class TablePerHierarchyStorageService<T> : BaseStorageService<T> where T : MicroUrlBaseEntity, new()
    {
        private readonly IStorageFactory _storageFactory;

        public const string StorageKey = "microurl";
        
        private const string EnabledKey = "enabled";
        private const string CreatedKey = "created";
        private const string TypeKey = "type";

        public TablePerHierarchyStorageService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }

        protected abstract string Kind { get; }

        protected abstract void MapToProperties(T entity, MapField<string, Value> properties);

        protected abstract void MapToEntity(MapField<string, Value> properties, T entity);

        public async Task<string> CreateAsync(T entity)
        {
            var dataStore = _storageFactory.GetStorage();
            var key = dataStore.CreateKeyFactory(StorageKey).CreateKey(entity.Key);
            var newEntity = new Entity { Key = key };
            
            newEntity.Properties.Add(EnabledKey, entity.Enabled);
            newEntity.Properties.Add(CreatedKey, entity.Created);
            newEntity.Properties.Add(TypeKey, Kind);

            MapToProperties(entity, newEntity.Properties);

            await dataStore.InsertAsync(newEntity);
            return key.Path.First().Name;
        }

        public async Task<T> LoadAsync(string key)
        {
            var dataStore = _storageFactory.GetStorage();
            var result = await dataStore.LookupAsync(GetKey(key));
            if (!Exists(result))
            {
                return null;
            }

            var instance = new T
            {
                Created = result[CreatedKey].TimestampValue, Enabled = result[EnabledKey].BooleanValue,
                Key = result.Key.Path.First().Name
            };

            MapToEntity(result.Properties, instance);

            return instance;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var dataStore = _storageFactory.GetStorage();
            var result = await dataStore.LookupAsync(GetKey(key));
            return Exists(result);
        }
        
        private bool Exists(Entity entity) => 
            entity != null && entity[TypeKey].StringValue == Kind;

        private static Key GetKey(string key) => new Key().WithElement(StorageKey, key);
    }
}