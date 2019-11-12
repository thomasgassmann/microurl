namespace MicroUrl.Storage.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserPassword
    {
        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }

        public int Iterations { get; set; }
    }
}
