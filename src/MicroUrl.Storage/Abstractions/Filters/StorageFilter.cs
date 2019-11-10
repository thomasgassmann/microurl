namespace MicroUrl.Storage.Abstractions.Filters
{
    using System;
    using System.Linq.Expressions;

    public abstract class StorageFilter
    {
        public static StorageFilter And(params StorageFilter[] filters) =>
            new AndFilter(filters);

        public static StorageFilter Or(params StorageFilter[] filters) =>
            new OrFilter(filters);

        public static StorageFilter Equals<T>(Expression<Func<T, object>> property, object value) =>
            new ComparisonFilter(
                new PropertyFilter<T>(property),
                new StaticValueFilter(value),
                ComparisonType.Equals);
    }
}