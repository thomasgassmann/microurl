namespace MicroUrl.Storage.Abstractions.Implementation
{
    public class DefaultKeyFactory : IKeyFactory
    {
        public IKey CreateFromString(string key) => DefaultKey.FromString(key);

        public IKey CreateNewAutoId() => DefaultKey.FromId(null);

        public IKey CreateFromId(long id) => DefaultKey.FromId(id);
    }
}