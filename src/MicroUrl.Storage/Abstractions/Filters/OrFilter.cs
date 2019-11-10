namespace MicroUrl.Storage.Abstractions.Filters
{
    public class OrFilter : StorageFilter
    {
        public OrFilter(params StorageFilter[] filters) =>
            Filters = filters;

        public StorageFilter[] Filters { get; }
    }
}