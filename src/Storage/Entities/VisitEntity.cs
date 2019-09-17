namespace MicroUrl.Storage.Entities
{
    using Google.Protobuf.WellKnownTypes;

    public class VisitEntity
    {
        public long Id { get; set; }
        
        public string Key { get; set; }

        public string Headers { get; set; }

        public Timestamp Created { get; set; }

        public string Ip { get; set; }
    }
}