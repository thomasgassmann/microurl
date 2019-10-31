namespace MicroUrl.Storage.Abstractions.Shared
{
    public interface IEntityAnalyzer
    {
        KeyType GetKeyType<T>();

        string GetEntityName<T>();

        IKey GetKeyValue<T>(T entity);
    }
}
