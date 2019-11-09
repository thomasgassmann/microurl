namespace MicroUrl.Storage.Abstractions.Shared
{
    using System;

    public class PropertySerializationInfo<T>
    {
        public string Property { get; set; }

        public bool IsKey { get; set; }

        public Action<T, object> Set { get; set; }

        public Func<T, object> Get { get; set; }

        public PropertyType PropertyType { get; set; }

        public bool ExcludeFromIndexes { get; set; }
    }
}