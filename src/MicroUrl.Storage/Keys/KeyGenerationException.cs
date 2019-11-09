namespace MicroUrl.Storage
{
    using MicroUrl.Common;

    public class KeyGenerationException : MicroUrlException
    {
        public KeyGenerationException(string message) : base(message)
        {
        }
    }
}