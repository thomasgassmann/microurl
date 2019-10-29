namespace MicroUrl.Storage.Entities
{
    using System;

    public class VisitEntity
    {
        public long Id { get; set; }
        
        public string Key { get; set; }

        public string Headers { get; set; }

        public DateTime Created { get; set; }

        public string Ip { get; set; }
    }
}