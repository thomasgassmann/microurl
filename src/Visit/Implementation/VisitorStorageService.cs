namespace MicroUrl.Visit.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
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

        public async Task<VisitorNumbers> GetVisitorCount(string key, DateTime from, DateTime to)
        {
            var storage = _storageFactory.GetStorage();
            var query = new Query(Kind)
            {
                Filter = Filter.And(
                    Filter.GreaterThanOrEqual("created", Timestamp.FromDateTime(@from)),
                    Filter.LessThanOrEqual("created", Timestamp.FromDateTime(to)),
                    Filter.Equal("key", key))
            };

            var enumerator = storage.RunQueryLazilyAsync(query).GetEnumerator();
            var hits = new Dictionary<string, long>();
            while (await enumerator.MoveNext())
            {
                var currentIp = enumerator.Current["ip"].StringValue;
                if (!hits.ContainsKey(currentIp))
                {
                    hits.Add(currentIp, 0);
                }

                hits[currentIp]++;
            }

            return new VisitorNumbers
            {
                VisitorCount = hits.Sum(x => x.Value),
                UniqueVisitorCount = hits.Keys.Count
            };
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