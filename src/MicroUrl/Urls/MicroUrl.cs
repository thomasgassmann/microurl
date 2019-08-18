namespace MicroUrl.Urls
{
    using System;

    public class MicroUrl
    {
        public string Key { get; set; }
        
        public string Url { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }
    }
}