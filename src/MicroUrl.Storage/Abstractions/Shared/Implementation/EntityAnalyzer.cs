namespace MicroUrl.Storage.Abstractions.Shared.Implementation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class EntityAnalyzer : IEntityAnalyzer
    {
        private readonly IDictionary<Type, PropertyInfo> _keyPropertyMap = new ConcurrentDictionary<Type, PropertyInfo>();
        private readonly IDictionary<Type, IList<object>> _serializationInfoMap = new ConcurrentDictionary<Type, IList<object>>();

        private readonly IDictionary<Type, EntityNameAttribute> _entityNameMap =
            new ConcurrentDictionary<Type, EntityNameAttribute>();

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

        public IList<PropertySerializationInfo<T>> GetSerializationInfo<T>() =>
            LoadAndCacheInDictionary<T, IList<object>, IList<PropertySerializationInfo<T>>>(
                _serializationInfoMap,
                () =>
                {
                    var list = new List<object>();

                    var keyProperty = GetKeyProperty<T>();
                    var properties = typeof(T).GetProperties()
                        .Select(x => new PropertySerializationInfo<T>
                        {
                            Get = obj => x.GetValue(obj),
                            Set = (obj, value) => x.SetValue(obj, value),
                            Property = TransformPropertyName(x.Name),
                            ExcludeFromIndexes = x.GetCustomAttribute<ExcludeFromIndexesAttribute>() != null,
                            PropertyType = GetPropertyType(x),
                            IsKey = keyProperty.Name == x.Name
                        });

                    list.AddRange(properties);
                    return list;
                },
                x => x.Select(x => (PropertySerializationInfo<T>) x).ToList());

        public PropertySerializationInfo<T> GetSerializationInfo<T>(Expression<Func<T, object>> property)
        {
            var expr = property.Body as MemberExpression;
            if (expr == null)
            {
                throw new ArgumentException("Must be member expression");
            }

            var memberName = TransformPropertyName(expr.Member.Name);
            var allSerializationInfo = GetSerializationInfo<T>();
            return allSerializationInfo.FirstOrDefault(x => x.Property == memberName);
        }

        public string GetEntityName<T>() =>
            LoadAndCacheInDictionary<T, EntityNameAttribute, string>(
                _entityNameMap,
                () => typeof(T).GetCustomAttributes(true).FirstOrDefault(x => x is EntityNameAttribute) as
                    EntityNameAttribute,
                x => x.Name);

        public KeyType GetKeyType<T>() =>
            ((KeyAttribute) GetKeyProperty<T>().GetCustomAttributes(typeof(KeyAttribute)).First()).KeyType;

        private string TransformPropertyName(string input) =>
            input.ToLowerInvariant();

        private PropertyType GetPropertyType(PropertyInfo info)
        {
            if (info.PropertyType == typeof(string))
            {
                return PropertyType.String;
            }

            if (info.PropertyType == typeof(int) || info.PropertyType == typeof(long))
            {
                return PropertyType.Long;
            }

            if (info.PropertyType == typeof(float) || info.PropertyType == typeof(double))
            {
                return PropertyType.Double;
            }

            if (info.PropertyType == typeof(DateTime))
            {
                return PropertyType.DateTime;
            }

            if (info.PropertyType == typeof(bool))
            {
                return PropertyType.Boolean;
            }

            throw new ArgumentException();
        }

        private PropertyInfo GetKeyProperty<T>() =>
            LoadAndCacheInDictionary<T, PropertyInfo, PropertyInfo>(
                _keyPropertyMap,
                () => typeof(T).GetProperties()
                    .FirstOrDefault(x => x.GetCustomAttributes(true).Any(x => x is KeyAttribute)),
                x => x);

        private TResult LoadAndCacheInDictionary<T, TSave, TResult>(IDictionary<Type, TSave> cache, Func<TSave> load,
            Func<TSave, TResult> convert) where TSave : class
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