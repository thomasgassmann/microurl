namespace MicroUrl.Storage.Abstractions
{
    public interface IStorageFactory
    {
        IStorage<T> CreateStorage<T>() where T : class, new();
    }
}