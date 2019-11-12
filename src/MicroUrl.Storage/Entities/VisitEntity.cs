namespace MicroUrl.Storage.Entities
{
    using System;
    using MicroUrl.Storage.Abstractions;

    [EntityName("visit")]
    internal class VisitEntity
    {
        [Key(KeyType.AutoId)]
        public long Id { get; set; }
        
        public string Key { get; set; }

        [ExcludeFromIndexes]
        public string Headers { get; set; }

        public string Ip { get; set; }

        public DateTime Created { get; set; }
    }
}