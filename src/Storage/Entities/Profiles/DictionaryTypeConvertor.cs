namespace MicroUrl.Storage.Entities.Profiles
{
    using AutoMapper;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;

    public abstract class DictionaryTypeConvertor<T> : ITypeConverter<T, MapField<string, Value>>
    {
        public abstract void Map(T source, MapField<string, Value> destination);

        public MapField<string, Value> Convert(T source, MapField<string, Value> destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new MapField<string, Value>();
            }

            Map(source, destination);
            return destination;
        }
    }
}
