namespace MicroUrl.Storage.Abstractions.Shared
{
    using System.Collections.Generic;

    public interface IEntityAnalyzer
    {
        KeyType GetKeyType<T>();

        string GetEntityName<T>();

        IKey GetKeyValue<T>(T entity);

        IList<PropertySerializationInfo<T>> GetSerializationInfo<T>();
    }
}
