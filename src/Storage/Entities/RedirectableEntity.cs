namespace MicroUrl.Storage.Entities
{
    using System;
    using MicroUrl.Storage.Abstractions;

    [EntityName("microurl")]
    public class RedirectableEntity
    {
        [Key(KeyType.StringId)]
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }
    }
}