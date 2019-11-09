namespace MicroUrl.Common
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