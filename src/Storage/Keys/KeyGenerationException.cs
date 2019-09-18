namespace MicroUrl.Urls
{
    using MicroUrl.Infrastructure;

    public class KeyGenerationException : MicroUrlException
    {
        public KeyGenerationException(string message) : base(message)
        {
        }
    }
}