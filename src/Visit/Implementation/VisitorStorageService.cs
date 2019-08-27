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

//        public async IAsyncEnumerable<UrlVisitEntity> GetVisitorCountAsync(string key, DateTime from, DateTime to)
//        {
//            var storage = _storageFactory.GetStorage();
//            var query = new Query(Kind)
//            {
//                Filter = Filter.And(
//                    Filter.GreaterThanOrEqual("created", Timestamp.FromDateTime(@from)),
//                    Filter.LessThanOrEqual("created", Timestamp.FromDateTime(to)),
//                    Filter.Equal("key", key))
//            };
//
//            var enumerator = storage.RunQueryLazilyAsync(query).GetEnumerator();
//            while (await enumerator.MoveNext())
//            {
//                yield return new UrlVisitEntity
//                {
//                    Id = enumerator.Current.Key.Path.First().Id,
//                    Created = enumerator.Current.Properties["created"].TimestampValue,
//                    Headers = enumerator.Current.Properties["headers"].StringValue,
//                    Ip = enumerator.Current.Properties["ip"].StringValue,
//                    Key = key
//                };
//            }
//        }
        
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