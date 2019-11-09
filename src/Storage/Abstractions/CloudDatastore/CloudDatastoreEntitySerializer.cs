namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using Google.Cloud.Datastore.V1;
    using MicroUrl.Storage.Abstractions.Shared;

    public class CloudDatastoreEntitySerializer<T> : IEntitySerializer<T, Entity>
    {
        private readonly IEntityAnalyzer _analyzer;
        
        public CloudDatastoreEntitySerializer(IEntityAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        public void Serialize(T source, Entity destination)
        {
            var serializationInfo = _analyzer.GetSerializationInfo<T>();
            foreach (var info in serializationInfo)
            {
                var value = info.Get(source);
                if (info.ExcludeFromIndexes)
                {
                    destination.Properties.Add(info.Property, new Value
                    {
                        
                    });
                }
            }
        }

        public void Deserialize(Entity source, T destination)
        {
            throw new System.NotImplementedException();
        }
    }
}