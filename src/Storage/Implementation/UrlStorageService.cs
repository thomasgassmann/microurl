namespace MicroUrl.Storage.Implementation
{
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public class UrlStorageService : TablePerHierarchyStorageService<MicroUrlEntity>
    {
        private const string KindKey = "url";
        private const string UrlKey = "url";
        
        public UrlStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected override string Kind => KindKey;

        protected override void MapToProperties(MicroUrlEntity entity, MapField<string, Value> properties)
        {
            properties.Add(UrlKey, entity.Url);
        }

        protected override void MapToEntity(MapField<string, Value> properties, MicroUrlEntity entity)
        {
            entity.Url = properties[UrlKey].StringValue;
        }
    }
}