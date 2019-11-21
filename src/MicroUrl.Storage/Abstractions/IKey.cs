namespace MicroUrl.Storage.Abstractions
{
    public interface IKey
    {
        string StringValue { get; }

        long LongValue { get; }

        KeyType KeyType { get; }
    }
}
