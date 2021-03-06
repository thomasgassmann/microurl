namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using System;
    using System.Linq;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.WellKnownTypes;
    using MicroUrl.Storage.Abstractions.Shared;
    using Value = Google.Cloud.Datastore.V1.Value;

    public class CloudDatastoreEntitySerializer<T>
    {
        private readonly IEntityAnalyzer _analyzer;
        
        public CloudDatastoreEntitySerializer(IEntityAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        public void Serialize(T source, Entity destination)
        {
            var serializationInfo = _analyzer.GetSerializationInfo<T>();
            foreach (var info in serializationInfo.Where(x => !x.IsKey))
            {
                var sourceValue = info.Get(source);
                if (sourceValue == null)
                {
                    continue;
                }
                
                var propertyValue = GetValueFromPropertyValue(info.PropertyType, sourceValue);
                propertyValue.ExcludeFromIndexes = info.ExcludeFromIndexes;

                destination.Properties.Add(info.Property, propertyValue);
            }
        }

        public void Deserialize(Entity source, T destination)
        {
            var serializationInfo = _analyzer.GetSerializationInfo<T>();
            foreach (var info in serializationInfo.Where(x => !x.IsKey))
            {
                if (!source.Properties.TryGetValue(info.Property, out Value value))
                {
                    // TODO: log
                    continue;
                }

                var propValue = GetPropertyValueFromValue(info.PropertyType, value);
                info.Set(destination, propValue);
            }

            var keyType = _analyzer.GetKeyType<T>();
            var keyInfo = serializationInfo.Single(x => x.IsKey);
            var firstPath = source.Key.Path.First();
            keyInfo.Set(destination, keyType switch
            {
                KeyType.AutoId => (object)firstPath.Id,
                KeyType.StringId => (object)firstPath.Name,
                _ => throw new ArgumentException()
            });
        }

        public object GetPropertyValueFromValue(PropertyType type, Value value) =>
            type switch
            {
                PropertyType.Double => (object) value.DoubleValue,
                PropertyType.Long => (object) value.IntegerValue,
                PropertyType.String => (object) value.StringValue,
                PropertyType.DateTime => (object) value.TimestampValue.ToDateTime(),
                PropertyType.Boolean => (object) value.BooleanValue,
                _ => throw new ArgumentException(type.ToString())
            };

        public Value GetValueFromPropertyValue(PropertyType type, object value) =>
            type switch
            {
                PropertyType.Double => new Value
                {
                    DoubleValue = (double) value
                },
                PropertyType.Long => new Value
                {
                    IntegerValue = (long) value
                },
                PropertyType.String => new Value
                {
                    StringValue = (string) value
                },
                PropertyType.DateTime => new Value
                {
                    TimestampValue = Timestamp.FromDateTime(((DateTime) value).ToUniversalTime())
                },
                PropertyType.Boolean => new Value
                {
                    BooleanValue = (bool)value
                },
                _ => throw new ArgumentException(type.ToString())
            };
    }
}