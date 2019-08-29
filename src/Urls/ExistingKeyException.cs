namespace MicroUrl.Urls
{
    using MicroUrl.Infrastructure;

    public class ExistingKeyException : MicroUrlException
    {
        public ExistingKeyException(string key)
            : base($"Key already exists: {key}")
        {
        }
    }
}