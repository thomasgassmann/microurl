namespace MicroUrl.Storage.Abstractions.Filters
{
    public class AndFilter : StorageFilter
    {
        public AndFilter(params StorageFilter[] filters)
        {
            Filters = filters;
        }

        public StorageFilter[] Filters { get; }
    }
}