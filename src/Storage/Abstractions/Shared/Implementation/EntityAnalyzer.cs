namespace MicroUrl.Storage.Abstractions.Shared.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityAnalyzer : IEntityAnalyzer
    {
        private readonly IDictionary<Type, KeyType> _keyTypeMap = new Dictionary<Type, KeyType>();

        public IKey GetKeyValue<T>(T entity)
        {

        }

        public string GetEntityName<T>()
        {

        }

        public KeyType GetKeyType<T>()
        {
            if (_keyTypeMap.TryGetValue(typeof(T), out var keyType))
            {
                return keyType;
            }

            var property = typeof(T).GetProperties()
                .Select(x => x.GetCustomAttributes(true))
                .SelectMany(x => x.Select(p => p as KeyAttribute))
                .FirstOrDefault(x => x != null);
            if (property == null)
            {
                throw new InvalidOperationException("Cannot operate on entity without key");
            }

            _keyTypeMap.Add(typeof(T), property.KeyType);
            return property.KeyType;
        }
    }
}
