namespace MicroUrl.Storage.Abstractions.Shared.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class EntityAnalyzer : IEntityAnalyzer
    {
        private readonly IDictionary<Type, PropertyInfo> _keyPropertyMap = new Dictionary<Type, PropertyInfo>();
        private readonly IDictionary<Type, EntityNameAttribute> _entityNameMap = new Dictionary<Type, EntityNameAttribute>();

        private readonly IKeyFactory _keyFactory;
        
        public EntityAnalyzer(IKeyFactory keyFactory) =>
            _keyFactory = keyFactory;
        
        public IKey GetKeyValue<T>(T entity)
        {
            var propertyValue = GetKeyProperty<T>().GetValue(entity);
            var keyType = GetKeyType<T>();
            if (propertyValue == null && keyType == KeyType.AutoId)
            {
                return _keyFactory.CreateNewAutoId();
            }

            if (propertyValue == null)
            {
                throw new InvalidOperationException();
            }
            
            switch (keyType)
            {
                case KeyType.AutoId:
                    return _keyFactory.CreateFromId((long) propertyValue);
                case KeyType.StringId:
                    return _keyFactory.CreateFromString((string) propertyValue);
                default:
                    throw new ArgumentException();
            }
        }

        public string GetEntityName<T>() =>
            LoadAndCacheInDictionary<T, EntityNameAttribute, string>(
                _entityNameMap,
                () => typeof(T).GetCustomAttributes(true).FirstOrDefault(x => x is EntityNameAttribute) as EntityNameAttribute,
                x => x.Name);

        public KeyType GetKeyType<T>() =>
            ((KeyAttribute) GetKeyProperty<T>().GetCustomAttributes(typeof(KeyAttribute)).First()).KeyType;

        private PropertyInfo GetKeyProperty<T>() =>
            LoadAndCacheInDictionary<T, PropertyInfo, PropertyInfo>(
                _keyPropertyMap,
                () => typeof(T).GetProperties()
                    .FirstOrDefault(x => x.GetCustomAttributes(true).Any(x => x is KeyAttribute)),
                x => x);

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
