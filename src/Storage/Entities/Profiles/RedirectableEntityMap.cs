namespace MicroUrl.Storage.Entities.Profiles
{
    using AutoMapper;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

    public class RedirectableEntityMap : Profile
    {
        private const string CreatedKey = "Created";
        private const string EnabledKey = "Created";
        private const string KeyKey = "Created";

        public RedirectableEntityMap()
        {
            CreateMap<MapField<string, Value>, RedirectableEntity>()
                .ForMember(x => x.Created, x => x.MapFrom(p => p[CreatedKey].ToDateTimeFromProjection()))
                .ForMember(x => x.Enabled, x => x.MapFrom(p => p[EnabledKey].BooleanValue))
                .ForMember(x => x.Key, x => x.MapFrom(p => p[KeyKey].StringValue));

            CreateMap<RedirectableEntity, MapField<string, Value>>()
                .ConvertUsing<RedirectableEntityDictionaryTypeConvertor>();
        }

        private class RedirectableEntityDictionaryTypeConvertor : DictionaryTypeConvertor<RedirectableEntity>
        {
            public override void Map(RedirectableEntity source, MapField<string, Value> destination)
            {
                destination.Add(CreatedKey, Timestamp.FromDateTime(source.Created));
                destination.Add(EnabledKey, source.Enabled);
                destination.Add(KeyKey, source.Key);
            }
        }
    }
}
