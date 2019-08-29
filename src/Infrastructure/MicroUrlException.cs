namespace MicroUrl.Infrastructure
{
    using System;

    public class MicroUrlException : Exception
    {
        public MicroUrlException(string message)
            : base(message)
        {
        }
    }
}