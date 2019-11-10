namespace MicroUrl.Storage.Abstractions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEntityAnalyzer
    {
        KeyType GetKeyType<T>();

        string GetEntityName<T>();

        IKey GetKeyValue<T>(T entity);

        IList<PropertySerializationInfo<T>> GetSerializationInfo<T>();

        PropertySerializationInfo<T> GetSerializationInfo<T>(Expression<Func<T, object>> property);
    }
}
