namespace MicroUrl.Storage.Entities
{
    using Google.Protobuf.WellKnownTypes;

    public class RedirectableEntity
    {
        public string Key { get; set; }

        public Timestamp Created { get; set; }

        public bool Enabled { get; set; }
    }
}