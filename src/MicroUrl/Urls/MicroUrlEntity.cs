namespace MicroUrl.Urls
{
    using System;
    using Google.Protobuf.WellKnownTypes;

    public class MicroUrlEntity
    {
        public string Key { get; set; }
        
        public string Url { get; set; }

        public Timestamp Created { get; set; }

        public bool Enabled { get; set; }
    }
}