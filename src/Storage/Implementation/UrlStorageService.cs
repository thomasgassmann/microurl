namespace MicroUrl.Storage.Implementation
{
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public class UrlStorageService : UrlBaseStorageService<MicroUrlEntity>
    {
        private const string UrlKey = "url";
        
        public UrlStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected override string UrlKind => "url";

        protected override void MapToProperties(MicroUrlEntity entity, MapField<string, Value> properties)
        {
            base.MapToProperties(entity, properties);
            properties.Add(UrlKey, entity.Url);
        }

        protected override void MapToEntity(MapField<string, Value> properties, MicroUrlEntity entity, Key key)
        {
            base.MapToEntity(properties, entity, key);
            entity.Url = properties[UrlKey].StringValue;
        }
    }
}