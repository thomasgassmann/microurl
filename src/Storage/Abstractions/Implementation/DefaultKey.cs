namespace MicroUrl.Storage.Abstractions.Implementation
{
    public class DefaultKey : IKey
    {
        private DefaultKey(string value)
        {
            KeyType = KeyType.StringId;
            StringValue = value;
        }

        private DefaultKey(long? value)
        {
            KeyType = KeyType.AutoId;
            LongValue = value;
        }
        
        public string StringValue { get; }
        
        public long? LongValue { get; }
        
        public KeyType KeyType { get; }

        public static DefaultKey FromString(string value) => new DefaultKey(value);

        public static DefaultKey FromId(long? value) => new DefaultKey(value);
    }
}