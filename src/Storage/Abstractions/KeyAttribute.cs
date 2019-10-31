namespace MicroUrl.Storage.Abstractions
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public KeyAttribute(KeyType keyType) =>
            KeyType = keyType;

        public KeyType KeyType { get; set; }
    }
}
