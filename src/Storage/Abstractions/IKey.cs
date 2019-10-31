namespace MicroUrl.Storage.Abstractions
{
    public interface IKey
    {
        string StringValue { get; set; }

        long? LongValue { get; set; }

        public bool IsNew => StringValue == null && LongValue == null;
    }
}
