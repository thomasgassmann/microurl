namespace MicroUrl.Storage.Implementation
{
    using System.Linq;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public abstract class UrlBaseStorageService<T> : BaseStorageService<T, string> where T : MicroUrlBaseEntity, new()
    {
        public const string MicroUrlStorageKey = "microurl";
        
        private const string EnabledKey = "enabled";
        private const string CreatedKey = "created";
        private const string TypeKey = "type";

        protected UrlBaseStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected abstract string UrlKind { get; }

        protected override string StorageKey => MicroUrlStorageKey;
        
        protected override Key GetNewKey(KeyFactory keyFactory, T entity) =>
            keyFactory.CreateKey(entity.Key);

        protected override string GetKey(Key key) =>
            key.Path.First().Name;

        protected override bool LogicalExists(Entity entity) =>
            entity != null && entity[TypeKey].StringValue == UrlKind;

        protected override void MapToProperties(T entity, MapField<string, Value> properties)
        {
            properties.Add(EnabledKey, entity.Enabled);
            properties.Add(CreatedKey, entity.Created);
            properties.Add(TypeKey, UrlKind);
        }

        protected override void MapToEntity(MapField<string, Value> properties, T entity, Key key)
        {
            entity.Key = GetKey(key);
            entity.Created = properties[CreatedKey].TimestampValue;
            entity.Enabled = properties[EnabledKey].BooleanValue;
        }
    }
}