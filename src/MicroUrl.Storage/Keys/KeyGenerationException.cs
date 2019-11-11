namespace MicroUrl.Storage.Keys
{
    using MicroUrl.Common;

    public class KeyGenerationException : MicroUrlException
    {
        public KeyGenerationException(string message) : base(message)
        {
        }
    }
}