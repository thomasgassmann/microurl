namespace MicroUrl.Storage.Entities.Profiles
{
    using AutoMapper;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;

    public abstract class DictionaryTypeConvertor<T> : ITypeConverter<T, Entity>
    {
        public abstract void MapProperties(T source, MapField<string, Value> destination);

        public abstract void MapKey(T source, Entity entity);

        public Entity Convert(T source, Entity destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new Entity();
            }

            MapProperties(source, destination.Properties);
            MapKey(source, destination);
            return destination;
        }
    }
}
