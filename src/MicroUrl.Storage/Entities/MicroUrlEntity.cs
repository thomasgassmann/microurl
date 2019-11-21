namespace MicroUrl.Storage.Entities
{
    using System;
    using MicroUrl.Storage.Abstractions;

    [EntityName("microurl")]
    internal class MicroUrlEntity
    {
        public const string UrlType = "url";
        public const string TextType = "text";

        // shared
        [Key(KeyType.StringId)]
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }

        public string Type { get; set; }

        // url entity
        public string Url { get; set; }

        // text entity
        public string Language { get; set; }

        [ExcludeFromIndexes]
        public string Text { get; set; }
    }
}