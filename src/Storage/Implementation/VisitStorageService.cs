namespace MicroUrl.Storage.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public class VisitStorageService : BaseStorageService<VisitEntity, long>, IVisitStorageService
    {
        private const string CreatedKey = "created";
        private const string KeyKey = "key";
        private const string HeadersKey = "headers";
        private const string IpKey = "ip";
        
        public VisitStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected override string StorageKey => "visit";

        public IAsyncEnumerable<VisitEntity> GetVisitorCountAsync(string key, DateTime @from, DateTime to)
        {            
            var query = new Query(StorageKey)
            {
                Filter = Filter.And(Filter.Equal(KeyKey, key))
            };

            return ExecuteQueryAsync(query);
        }

        protected override Key GetNewKey(KeyFactory keyFactory, VisitEntity entity) =>
            keyFactory.CreateIncompleteKey();

        protected override long GetKey(Key key) =>
            key.Path.First().Id;

        protected override bool LogicalExists(Entity entity) => true;

        protected override void MapToProperties(VisitEntity entity, MapField<string, Value> properties)
        {
            properties.Add(CreatedKey, entity.Created);
            properties.Add(KeyKey, entity.Key);
            properties.Add(HeadersKey, entity.Headers);
            properties.Add(IpKey, entity.Ip);
        }

        protected override void MapToEntity(MapField<string, Value> properties, VisitEntity entity, Key key)
        {
            entity.Id = GetKey(key);
            entity.Created = properties[CreatedKey].TimestampValue;
            entity.Key = properties[KeyKey].StringValue;
            entity.Headers = properties[HeadersKey].StringValue;
            entity.Ip = properties[IpKey].StringValue;
        }
    }
}