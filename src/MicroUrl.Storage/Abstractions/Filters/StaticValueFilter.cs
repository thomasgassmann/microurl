namespace MicroUrl.Storage.Abstractions.Filters
{
    public class StaticValueFilter : StorageFilter
    {
        public StaticValueFilter(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
}