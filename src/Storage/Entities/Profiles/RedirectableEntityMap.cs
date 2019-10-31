namespace MicroUrl.Storage.Entities.Profiles
{
    using AutoMapper;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using System.Linq;
    using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

    public class RedirectableEntityMap : Profile
    {
        private const string CreatedKey = "Created";
        private const string EnabledKey = "Created";
        private const string KeyKey = "Created";

        public RedirectableEntityMap()
        {
            CreateMap<Entity, RedirectableEntity>()
                .ForMember(x => x.Created, x => x.MapFrom(p => p.Properties[CreatedKey].ToDateTimeFromProjection()))
                .ForMember(x => x.Enabled, x => x.MapFrom(p => p.Properties[EnabledKey].BooleanValue))
                .ForMember(x => x.Key, x => x.MapFrom(p => p.Key.Path.First().Name));

            CreateMap<RedirectableEntity, Entity>()
                .ConvertUsing<RedirectableEntityDictionaryTypeConvertor>();
        }

        private class RedirectableEntityDictionaryTypeConvertor : DictionaryTypeConvertor<RedirectableEntity>
        {
            public override void MapKey(RedirectableEntity source, Entity entity)
            {
            }

            public override void MapProperties(RedirectableEntity source, MapField<string, Value> destination)
            {
                destination.Add(CreatedKey, Timestamp.FromDateTime(source.Created));
                destination.Add(EnabledKey, source.Enabled);
                destination.Add(KeyKey, source.Key);
            }
        }
    }
}
