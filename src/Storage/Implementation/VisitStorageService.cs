namespace MicroUrl.Storage.Implementation
{
    using System.Linq;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public class VisitStorageService : BaseStorageService<VisitEntity>
    {
        public VisitStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected override string StorageKey => "visit";

        protected override Key GetNewKey(KeyFactory keyFactory, VisitEntity entity) =>
            keyFactory.CreateIncompleteKey();

        protected override string GetKeyString(Key key) =>
            key.Path.First().Id.ToString();

        protected override bool LogicalExists(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        protected override void MapToProperties(VisitEntity entity, MapField<string, Value> properties)
        {
            throw new System.NotImplementedException();
        }

        protected override void MapToEntity(MapField<string, Value> properties, VisitEntity entity, Key key)
        {
            throw new System.NotImplementedException();
        }
    }
}