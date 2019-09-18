namespace MicroUrl.Storage.Entities
{
    using Google.Protobuf.WellKnownTypes;

    public class MicroUrlBaseEntity
    {
        public string Key { get; set; }

        public Timestamp Created { get; set; }

        public bool Enabled { get; set; }
    }
}