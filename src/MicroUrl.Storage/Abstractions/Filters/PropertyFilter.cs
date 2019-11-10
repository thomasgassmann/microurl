namespace MicroUrl.Storage.Abstractions.Filters
{
    using System;
    using System.Linq.Expressions;

    public class PropertyFilter<T> : StorageFilter
    {
        public PropertyFilter(Expression<Func<T, object>> propertyExpression)
        {
            PropertyExpression = propertyExpression;
        }

        public Expression<Func<T, object>> PropertyExpression { get; }
    }
}