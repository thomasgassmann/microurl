namespace MicroUrl.Storage.Abstractions.Shared.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityAnalyzer : IEntityAnalyzer
    {
        private readonly IDictionary<Type, KeyAttribute> _keyTypeMap = new Dictionary<Type, KeyAttribute>();
        private readonly IDictionary<Type, EntityNameAttribute> _entityNameMap = new Dictionary<Type, EntityNameAttribute>();

        public IKey GetKeyValue<T>(T entity)
        {

        }

        public string GetEntityName<T>() =>
            LoadAndCacheInDictionary<T, EntityNameAttribute, string>(
                _entityNameMap,
                () => typeof(T).GetCustomAttributes(true).FirstOrDefault(x => x is EntityNameAttribute) as EntityNameAttribute,
                x => x.Name);

        public KeyType GetKeyType<T>() =>
            LoadAndCacheInDictionary<T, KeyAttribute, KeyType>(
                _keyTypeMap, 
                () => typeof(T).GetProperties()
                   .Select(x => x.GetCustomAttributes(true))
                   .SelectMany(x => x.Select(p => p as KeyAttribute))
                   .FirstOrDefault(x => x != null),
                x => x.KeyType);

        private TResult LoadAndCacheInDictionary<T, TSave, TResult>(IDictionary<Type, TSave> cache, Func<TSave> load, Func<TSave, TResult> convert) where TSave : class
        {
            if (cache.TryGetValue(typeof(T), out var res))
            {
                return convert(res);
            }

            var result = load();
            if (result == null)
            {
                throw new InvalidOperationException("Result cannot be null.");
            }

            cache.Add(typeof(T), result);
            return convert(result);
        }
    }
}
