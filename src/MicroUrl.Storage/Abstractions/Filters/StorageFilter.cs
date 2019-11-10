namespace MicroUrl.Storage.Abstractions.Filters
{
    using System;
    using System.Linq.Expressions;

    public abstract class StorageFilter
    {
        public static StorageFilter And(params StorageFilter[] filters) =>
            new AndFilter(filters);

        public static StorageFilter Equals<T>(Expression<Func<T, object>> property, object value) =>
            new ComparisonFilter<T>(
                property,
                value,
                ComparisonType.Equals);

        public static StorageFilter GreaterThan<T>(Expression<Func<T, object>> property, object value) =>
            new ComparisonFilter<T>(
                property,
                value,
                ComparisonType.GreaterThan);

        public static StorageFilter LessThan<T>(Expression<Func<T, object>> property, object value) =>
            new ComparisonFilter<T>(
                property,
                value,
                ComparisonType.LessThan);
    }
}