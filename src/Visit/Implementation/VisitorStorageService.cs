namespace MicroUrl.Visit.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.WellKnownTypes;
    using MicroUrl.Storage;

    public class VisitorStorageService : IVisitorStorageService
    {
        private const string Kind = "Visit";

        private readonly IStorageFactory _storageFactory;

        public VisitorStorageService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }

        public async Task<IEnumerable<UrlVisitEntity>> GetVisitorCountAsync(string key, DateTime from, DateTime to)
        {
            var storage = _storageFactory.GetStorage();
            var query = new Query(Kind)
            {
                Filter = Filter.And(Filter.Equal("key", key))
            };

            var queryResults = await storage.RunQueryAsync(query);

            return queryResults.Entities.Select(item => new UrlVisitEntity
                {
                    Id = item.Key.Path.First().Id,
                    Created = item.Properties["created"].TimestampValue,
                    Headers = item.Properties["headers"].StringValue,
                    Ip = item.Properties["ip"].StringValue,
                    Key = key
                })
                .ToList();
        }
        
        public async Task SaveAsync(UrlVisitEntity entity)
        {
            var storage = _storageFactory.GetStorage();
            await storage.InsertAsync(new Entity
            {
                Key = storage.CreateKeyFactory(Kind).CreateIncompleteKey(),
                Properties =
                {
                    { "key", entity.Key },
                    { "headers", entity.Headers },
                    { "ip", entity.Ip },
                    { "created", entity.Created }
                }
            });
        }
    }
}