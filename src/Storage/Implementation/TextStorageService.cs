namespace MicroUrl.Storage.Implementation
{
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.Collections;
    using MicroUrl.Storage.Entities;

    public class TextStorageService : UrlBaseStorageService<MicroTextEntity>, ITextStorageService
    {
        private const string LanguageKey = "language";
        private const string TextKey = "text";
        
        public TextStorageService(IStorageFactory storageFactory) : base(storageFactory)
        {
        }

        protected override string UrlKind => "text";

        protected override void MapToEntity(MapField<string, Value> properties, MicroTextEntity entity, Key key)
        {
            base.MapToEntity(properties, entity, key);
            entity.Language = properties[LanguageKey].StringValue;
            entity.Text = properties[TextKey].StringValue;
        }

        protected override void MapToProperties(MicroTextEntity entity, MapField<string, Value> properties)
        {
            base.MapToProperties(entity, properties);
            properties.Add(LanguageKey, entity.Language);
            properties.Add(TextKey, new Value
            {
                StringValue = entity.Text,
                ExcludeFromIndexes = true
            });
        }
    }
}