namespace MicroUrl.Storage.Dto
{
    using System;

    public class Visit
    {
        public long Id { get; set; }

        public string Key { get; set; }
        
        public string Headers { get; set; }

        public DateTime Created { get; set; }

        public string Ip { get; set; }
    }
}