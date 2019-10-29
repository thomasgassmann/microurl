namespace MicroUrl.Storage.Entities
{
    using System;

    public class RedirectableEntity
    {
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }
    }
}