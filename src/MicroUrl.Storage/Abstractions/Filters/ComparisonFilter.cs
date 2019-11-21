namespace MicroUrl.Storage.Abstractions.Filters
{
    using System;
    using System.Linq.Expressions;

    public class ComparisonFilter<T> : StorageFilter
    {
        public ComparisonFilter(Expression<Func<T, object>> property, object value, ComparisonType type)
        {
            Property = property;
            Value = value;
            ComparisonType = type;
        }

        public Expression<Func<T, object>> Property { get; }

        public ComparisonType ComparisonType { get; }

        public object Value { get; }
    }
}