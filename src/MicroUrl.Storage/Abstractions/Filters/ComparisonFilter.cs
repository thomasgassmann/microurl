namespace MicroUrl.Storage.Abstractions.Filters
{
    public class ComparisonFilter : StorageFilter
    {
        public ComparisonFilter(StorageFilter left, StorageFilter right, ComparisonType type)
        {
            Left = left;
            Right = right;
            ComparisonType = type;
        }

        public StorageFilter Left { get; }
        
        public ComparisonType ComparisonType { get; }

        public StorageFilter Right { get; }
    }
}